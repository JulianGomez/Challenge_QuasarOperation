using ApiQuasar.Adapters.Interfaces;
using ApiQuasar.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiQuasar.Adapters
{
    public class AdapterPositionSatellite : IAdapterPositionSatellite
    {
        public Position GetPositionByTrilateration_V1(IEnumerable<TrilaterationModel> satellites)
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
