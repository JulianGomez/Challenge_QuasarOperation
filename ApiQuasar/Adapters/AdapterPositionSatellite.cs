using ApiQuasar.Adapters.Interfaces;
using ApiQuasar.Exceptions;
using ApiQuasar.Model;
using ApiQuasar.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Threading.Tasks;

namespace ApiQuasar.Adapters
{
    public class AdapterPositionSatellite : IAdapterPositionSatellite
    {
        public Position GetPositionByTrilateration_V1(IEnumerable<TrilaterationModel> satellites)
        {
            List<TrilaterationModel> listS = satellites.ToList();

            //DECLARACION DE VARIABLES
            double[] P1 = new double[2];
            double[] P2 = new double[2];
            double[] P3 = new double[2];
            double[] ex = new double[2];
            double[] ey = new double[2];
            double[] p3p1 = new double[2];
            double jval = 0;
            double temp = 0;
            double ival = 0;
            double p3p1i = 0;
            double triptx;
            double tripty;
            double xval;
            double yval;
            double t1;
            double t2;
            double t3;
            double t;
            double exx;
            double d;
            double eyy;
            //PASAJE DE PUNTOS A VECTORES
            //POINT 1
            P1[0] = listS[0].Position.X;
            P1[1] = listS[0].Position.Y;
            //POINT 2
            P2[0] = listS[1].Position.X;
            P2[1] = listS[1].Position.Y;
            //POINT 3
            P3[0] = listS[2].Position.X;
            P3[1] = listS[2].Position.Y;

            //DISTANCIA ENTRE KENOBI Y LA NAVE
            double distance1 = (listS[0].Distance / 100000);
            //DISTANCIA ENTRE SKYWALTER Y LA NAVE
            double distance2 = (listS[1].Distance / 100000);
            //DISTANCIA ENTRE SATO Y LA NAVE
            double distance3 = (listS[2].Distance / 100000);
            for (int i = 0; i < P1.Length; i++)
            {
                t1 = P2[i];
                t2 = P1[i];
                t = t1 - t2;
                temp += (t * t);
            }
            d = Math.Sqrt(temp);
            for (int i = 0; i < P1.Length; i++)
            {
                t1 = P2[i];
                t2 = P1[i];
                exx = (t1 - t2) / (Math.Sqrt(temp));
                ex[i] = exx;
            }
            for (int i = 0; i < P3.Length; i++)
            {
                t1 = P3[i];
                t2 = P1[i];
                t3 = t1 - t2;
                p3p1[i] = t3;
            }
            for (int i = 0; i < ex.Length; i++)
            {
                t1 = ex[i];
                t2 = p3p1[i];
                ival += (t1 * t2);
            }
            for (int i = 0; i < P3.Length; i++)
            {
                t1 = P3[i];
                t2 = P1[i];
                t3 = ex[i] * ival;
                t = t1 - t2 - t3;
                p3p1i += (t * t);
            }
            for (int i = 0; i < P3.Length; i++)
            {
                t1 = P3[i];
                t2 = P1[i];
                t3 = ex[i] * ival;
                eyy = (t1 - t2 - t3) / Math.Sqrt(p3p1i);
                ey[i] = eyy;
            }
            for (int i = 0; i < ey.Length; i++)
            {
                t1 = ey[i];
                t2 = p3p1[i];
                jval += (t1 * t2);
            }
            xval = (Math.Pow(distance1, 2) - Math.Pow(distance2, 2) + Math.Pow(d, 2)) / (2 * d);
            yval = ((Math.Pow(distance1, 2) - Math.Pow(distance3, 2) + Math.Pow(ival, 2) + Math.Pow(jval, 2)) / (2 * jval)) - ((ival / jval) * xval);
            t1 = listS[0].Position.X;
            t2 = ex[0] * xval;
            t3 = ey[0] * yval;
            triptx = t1 + t2 + t3;
            t1 = listS[0].Position.Y;
            t2 = ex[1] * xval;
            t3 = ey[1] * yval;
            tripty = t1 + t2 + t3;

            triptx = Math.Round(triptx, 3);
            tripty = Math.Round(tripty, 3);


            return new Position(triptx, tripty);
        }


        public Position GetPositionByTrilateration_V2(IEnumerable<TrilaterationModel> satellites)
        {
            List<TrilaterationModel> listS = satellites.ToList();

            Position point1 = listS[0].Position;
            Position point2 = listS[1].Position;
            Position point3 = listS[2].Position;
            double r1 = listS[0].Distance;
            double r2 = listS[1].Distance;
            double r3 = listS[2].Distance;

            double p2p1Distance = Math.Pow(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2), 0.5);
            Position ex = new Position((point2.X - point1.X) / p2p1Distance, (point2.Y - point1.Y) / p2p1Distance);
            Position aux = new Position(point3.X - point1.X, point3.Y - point1.Y);
            //componente X
            double i = ex.X * aux.X + ex.Y * aux.Y;
            //the unit vector in the y direction. 
            Position aux2 = new Position (point3.X - point1.X - i * ex.X, point3.Y - point1.Y - i * ex.Y );
            Position ey = new Position (aux2.X / norm(aux2), aux2.Y / norm(aux2));
            //componente Y
            double j = ey.X * aux.X + ey.Y * aux.Y;
            //coordenadas
            double x = (Math.Pow(r1, 2) - Math.Pow(r2, 2) + Math.Pow(p2p1Distance, 2)) / (2 * p2p1Distance);
            double y = (Math.Pow(r1, 2) - Math.Pow(r3, 2) + Math.Pow(i, 2) + Math.Pow(j, 2)) / (2 * j) - i * x / j;
            //resultado 
            double finalX = point1.X + x * ex.X + y * ey.X;
            double finalY = point1.Y + x * ex.Y + y * ey.Y;

            return new Position(finalX, finalY);
        }


        private double norm(Position p) 
        {
            return Math.Pow(Math.Pow(p.X, 2) + Math.Pow(p.Y, 2), .5);
        }

    }
}
