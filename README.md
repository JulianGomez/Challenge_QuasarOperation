# Challenge Meli - Operación Fuego de Quasar

_El servicio de inteligencia rebelde ha detectado un llamado de auxilio de una nave portacarga imperial a la deriva en
un campo de asteroides. El manifiesto de la nave es ultra clasificado, pero se rumorea que transporta raciones y
armamento para una legión entera._

## General

Creación de una WebApi que permite retornar la fuente y contenido del mensaje de auxilio, en los cuales se cuenta con 3 satelites
para triangular la posicion y poder descifrar el mensaje completo a partir de transmisiones incompletas debido
al campo de asteroides que se encuentra frente a la nave.

## Datos del Proyecto

* El desarrollo fue realizado en código C#, utilizando Framework 5.0 ASP.NET Core.
* WebApi documentada con swagger, donde al contar con interfaz de usuario, se podrán ejecutar las peticiones HTTP en los endpoints defindos.


## Instalación local y ejecución

Ésta instrucción permitirá obtener una copia del proyecto para utilizarlo de forma local y poder realizar desarrollo / pruebas:
```
git clone https://github.com/JulianGomez/Challenge_QuasarOperation
```

La WebApi se encuentra hosteada en AWS para su ejecución:
```
http://ec2-34-220-99-102.us-west-2.compute.amazonaws.com/swagger/index.html
```


## Utilización

Usando Swagger: 

_http://ec2-34-220-99-102.us-west-2.compute.amazonaws.com/swagger/index.html_
  
Tambien se podría optar por aplicaciones como POSTMAN accediendo directamente a cada endpoint: 

* _http://ec2-34-220-99-102.us-west-2.compute.amazonaws.com/api/TopSecret
* http://ec2-34-220-99-102.us-west-2.compute.amazonaws.com/api/TopSecret_Split_

  
 
## Endpoints 

* **Topsecret (POST):** obtiene la posicion y el mensaje completo de la información obtenida por los satelites.
Sí el mensaje o posición no se puede recuperar deberá devolver un error 404 Not Found.

Ejemplo de request body:

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

Ejemplo de respuesta exitosa retornando code 200:

```
{
  "position": {
	"x": -487.2859125000017,
	"y": 1557.0142250000058
  },
  "message": "este es un mensaje secreto"
}
```

En caso que no se pueda determinar la posición o el mensaje, retorna code 404:

```
	{
	  "error": "No se puede calcular el mensaje con la información proporcionada."
	}
```



* **Topsecret_Split (POST):** recibirá en su header el nombre del satelite como parámetro de ruta. En su body recibirá la distancia y el mensaje incompleto.
Toda ésta información quedará guardada en memoria.

Ejemplo de request:

```
header:  "kenobi"

body:
{
  "distance": 100.0,
  "message": ["este", "", "", "mensaje", ""]
} 
```

Ejemplo de respuesta exitosa retornando code 200:

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

En caso que el nombre del satellite ingresado no corresponda a uno válido, retorna code 404:

```
{
  "error": "No es posible guardar el satélite 'julian'. No es un satelite válido."
}
```



* **Topsecret_Split (GET):** obtiene la posicion y el mensaje completo de la información guardada en memoria por el endpoint *Topsecret_Split (POST):*.
Sí la cantidad de satelites no es la correcta o el mensaje/posición no se puede recuperar deberá devolver un error 404 Not Found.

```
{
  "error": "No se puede calcular el mensaje con la información proporcionada."
}
```


