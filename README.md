# Programa de Gestión de Inventario de Carros

Este proyecto consiste en un programa de gestión de inventario de carros, desarrollado en C# 
con una base de datos MySQL. A continuación, se describe brevemente la funcionalidad del 
programa y su implementación.

## Base de Datos

La base de datos utilizada para este proyecto se llama `examenfinal` y contiene una tabla llamada `carros` con la siguiente estructura y sus tipos de dato:

```
CREATE DATABASE examenfinal;

USE examenfinal;

CREATE TABLE carros (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    Marca VARCHAR(255),
    Modelo VARCHAR(255),
    Año INT,
    Color VARCHAR(255),
    Precio DECIMAL(10,2),
    Descripcion VARCHAR(255),
    Disponible TINYINT(1),
    FechaIngreso DATE
);
```

## Descripción General 

El programa permite realizar las siguientes operaciones en la base de datos de carros:

1. **Conexión a la Base de Datos:**
   - Se creó la clase `ConexionMySql` para establecer la conexión a la base de datos MySQL.

2. **Insertar Nuevo Auto:**
   - Método `InsertarCarro` para añadir un nuevo carro a la base de datos utilizando parámetros SQL para asegurar
   la integridad de los datos.

3. **Manejo de Datos en el Formulario:**
   - Creación de instancias y listas de objetos `Carros` para representar y almacenar información de los carros.

4. **Obtener Registros:**
   - Método `ObtenerTodosLosCarros` que devuelve una lista de todos los carros en la base de datos.

5. **Actualizar Carro:**
   - Método `ActualizarCarro` para modificar la información de un carro existente.

6. **Eliminar Carro:**
   - Método `EliminarCarro` para eliminar un carro de la base de datos mediante su ID.

7. **Buscar Carro por ID:**
   - Método `BuscarCarroPorID` para buscar un carro específico usando su ID.

8. **Verificar Existencia de ID:**
   - Método `CarroExisteID` para verificar si un carro con un ID específico ya existe en la base de datos.

## Funciones del Formulario

El formulario de la aplicación permite las siguientes interacciones:

1. **Mostrar Todos los Carros:**
   - Función `MostrarTodosLosCarros` que actualiza un DataGridView con todos los carros de la base de datos.

2. **Mostrar Detalles de Carro:**
   - Función `MostrarRegistro` que muestra los detalles del carro seleccionado en los campos del formulario.

3. **Guardar Nuevo Carro:**
   - Función `GrabarRegistro` que guarda un nuevo carro en la base de datos.

4. **Actualizar Información de Carro:**
   - Función `ActualizarCarro` que actualiza la información de un carro existente.

5. **Limpiar Campos:**
   - Función `LimpiarCamposRegistro` que limpia los campos del formulario.

6. **Eliminar Carro:**
   - Función `EliminarRegistro` que elimina un carro de la base de datos.

7. **Buscar Carro por ID:**
   - Función `BuscarRegistroPorID` que busca un carro usando su ID y muestra sus detalles en el formulario.

8. **Salir del Programa**
   - Función `ConfirmarSalida` hace la pregunta si se desea salir, si la respuesta es si o no, se realizara su función.
   - 
## Navegación del Programa

El programa cuenta con varios botones para facilitar la interacción:

- `buttonTodos_Clic:` Muestra todos los registros en el formulario.
- `buttonModificar_Click` Actualiza el registro seleccionado.
- `buttonGrabarRegistro_Clic` Guarda un nuevo registro en la base de datos.
- `buttonEliminar_Click` Elimina el registro seleccionado.
- `buttonBuscarID_Click` Busca un carro por ID y muestra sus detalles.
- `buttonLimpiarRegistro_Click` Limpia los campos del formulario.
- `buttonSiguiente_Click` Permite ir al siguiente registro
- `buttonAnterior_Click` Permite ir al anterior registro
- `buttonSalir_Click` Confirma y cierra la aplicación.

## Manejo de Errores

Todas las operaciones incluyen manejo de excepciones mediante bloques `try-catch` para asegurar que cualquier error sea capturado y manejado adecuadamente.

## Conclusión

Este programa de gestión de inventario de carros proporciona una manera eficiente de manejar los registros de carros utilizando C# y MySQL. La estructura modular y el manejo de datos seguro aseguran una operación fiable y efectiva.













