# Challenge Meli - Operación Fuego de Quasar

_El servicio de inteligencia rebelde ha detectado un llamado de auxilio de una nave portacarga imperial a la deriva en
un campo de asteroides. El manifiesto de la nave es ultra clasificado, pero se rumorea que transporta raciones y
armamento para una legión entera._

## General

Creación de una WebApi que permite retornar la posición y contenido del mensaje de auxilio, en los cuales se cuenta con 3 satélites
para triangular la posición (método basado en [Trilateración 2D](https://www.pathpartnertech.com/triangulation-vs-trilateration-vs-multilateration-for-indoor-positioning-systems/)) y descifrado del mensaje completo a partir de transmisiones incompletas que llegan de los satélites.

## Datos del Proyecto

* El desarrollo fue realizado en código C#, utilizando el Framework 5.0 ASP.NET Core.
* WebApi documentada con swagger, donde al contar con interfaz de usuario, se podrán ejecutar las peticiones HTTP en los endpoints defindos.


## Instalación local y ejecución

Instrucción que permitirá obtener una copia del proyecto para utilizarlo de forma local y realizar pruebas:
```
git clone https://github.com/JulianGomez/Challenge_QuasarOperation
```
La WebApi se encuentra hosteada en AWS para su ejecución:
```
http://ec2-34-220-99-102.us-west-2.compute.amazonaws.com/swagger/index.html
```


## Utilización

Usando Swagger: 

* http://ec2-34-220-99-102.us-west-2.compute.amazonaws.com/swagger/index.html
  
Tambien se podría optar por la utilizacion de aplicaciones como POSTMAN o INSOMNIA accediendo a cada endpoint: 

* http://ec2-34-220-99-102.us-west-2.compute.amazonaws.com/api/TopSecret
* http://ec2-34-220-99-102.us-west-2.compute.amazonaws.com/api/TopSecret_Split


 
## Endpoints 

### **Topsecret (POST):** 

_Obtiene la posicion y el mensaje completo de la información obtenida de cada satélite.
Sí el mensaje o posición no se puede recuperar devolverá un error 404 Not Found._

Ejemplo - Request Body:

```
{
  "satellites": [
    {
      "name": "kenobi",
      "distance": 100.0,
      "message": ["este", "", "", "mensaje", ""]
    },
    {
      "name": "skywalker",
      "distance": 115.5,
      "message": ["", "es", "", "", "secreto"]
    },
    {
      "name": "sato",
      "distance": 142.7,
      "message": ["este", "", "un", "", ""]
    }
  ]
}
```

Ejemplo - respuesta exitosa con código 200:

```
{
  "position": {
	"x": -487.2859125000017,
	"y": 1557.0142250000058
  },
  "message": "este es un mensaje secreto"
}
```

En caso que no se pueda determinar la posición o el mensaje, retorna código 404:

```
{
  "error": "No se puede calcular el mensaje con la información proporcionada."
}
```



### **Topsecret_Split (POST):** 

_Recibirá en su header como parámetro el nombre del satelite. En su body recibirá la distancia y el mensaje incompleto.
En caso de ser un sátelite válido, la información quedará guardada en memoria._

Ejemplo - Request:

```
header:  "kenobi"

body:
{
  "distance": 100.0,
  "message": ["este", "", "", "mensaje", ""]
} 
```

Ejemplo - respuesta exitosa con código 200:

```
{
  "name": "kenobi",
  "distance": 100,
  "message": [
	"este",
	"",
	"",
	"mensaje",
	""
  ]
}
```

En caso que el nombre del satellite ingresado no corresponda a uno válido, retorna código 404:

```
{
  "error": "No es posible guardar el satélite 'julian'. No es un satelite válido."
}
```


### **Topsecret_Split (GET):** 

_Obtiene la posicion y el mensaje completo de la información guardada en memoria por el endpoint *[Topsecret_Split -> POST]*.
Sí la cantidad de satelites no es la correcta o el mensaje/posición no se puede recuperar devolverá un error 404 Not Found._

```
{
  "error": "No se puede calcular el mensaje con la información proporcionada."
}
```


