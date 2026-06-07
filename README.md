# AppEmermedica

Para ejecutar la aplicacion una vez que usted lo copie en su equipo local debe hacer lo siguiente:

  * Abrir con VS code o Visual Studio el appsettings.json configurar la cadena de conexion para tener acceso a la instancia de SQL.
  * Ejecutar en Visual Studio el proyecto AppEmermedica o VS code "dotnet run"
  * La base de datos se creara automaticamente con su respectiva tabla.
  * En el swagger encontrara los endpoint nesesarios dados en los ejemplos.
  * Para utilizar los endpoint debe generar un token en "Auth" en el swagger.
  * Ingresa un nombre y su rol (Admin o User).
  * Cuando genere el token copielo y se dirige al enpoint que quiere ejecutar y le da click al icono forma de candado, se despliega una ventana y ahy pega el token generado.
  * De esa forma usted se autentica y se autoriza lo que puede hacer.

# AppEmermedica Test

  * En visual studio seleccione el proyecto y click con el boton derecho y elija "Run Test".