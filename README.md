# 💊 API de Gestión de Farmacia (`Farmacia`)

API RESTful desarrollada en **ASP.NET Core (.NET 10.0)** para la administración integral de una farmacia. El proyecto implementa una arquitectura separada en capas, almacenamiento en memoria (RAM), validaciones mediante *DataAnnotations*, uso de interfaces, excepciones personalizadas y un *Middleware* centralizado para el manejo de errores.

**Autores:** Jimmy y Andres

---

## 🏗️ Estructura del Proyecto

```text
Farmacia/
├── Controllers/
│   ├── AuthController.cs
│   ├── ClienteControll.cs
│   ├── EmpleadosControll.cs
│   ├── ProductoControll.cs
│   ├── ProveedoresControll.cs
│   └── VentasControll.cs
├── Datos/
│   └── AppDbContext.cs
├── Dtos/
│   ├── Cliente/
│   │   ├── CreateCliente.cs
│   │   └── UpdateCliente.cs
│   ├── Empleado/
│   │   ├── CreateEmpleado.cs
│   │   └── UpdateEmpleado.cs
│   ├── Producto/
│   │   ├── CreateProducto.cs
│   │   └── UpdateProducto.cs
│   ├── Proveedor/
│   │   ├── CreateProveedor.cs
│   │   └── UpdateProveedor.cs
│   ├── Venta/
│   │   ├── CreateVenta.cs
│   │   └── UpdateVenta.cs
│   ├── jwt/ 
│   │   ├──LoginRequest.cs
│   │   ├──RegistroRequest.cs
│   │   └──TokenResponse.cs
│   └── ErrorResponse.cs
├── Excepciones/
│   ├── ConflictoException.cs
│   ├── CredencialesInvalidasException.cs
│   └── RecursoNoEncontradoException.cs
├── Middlewares/
│   └── ManejoErrores.cs
├── Migrations/
│   ├── 20261116232251_Inicial.cs
│   ├── 20261116232251_Inicial.Designer.cs 
│   ├── 20261117044759_RelacionClienteVentas.cs
│   ├── 20261117044759_RelacionClienteVentas.Designer.cs
│   ├── 20261120030340_AgregarIdentity.cs
│   ├── 20261120030340_AgregarIdentity.Designer.cs
│   └── AppDbContextModelSnapshot.cs
├── Modelos/
│   ├── Cliente.cs
│   ├── Empleados.cs
│   ├── Persona.cs
│   ├── Producto.cs
│   ├── Proovedor.cs
│   └── Ventas.cs
├── Postman/
│   ├── Clientes.postman_collection.json
│   ├── Empleado.postman_collection.json
│   ├── jwt.postman_collection.json
│   ├── Productos.postman_collection.json
│   ├── Proveedores.postman_collection.json
│   └── Ventassss.postman_collection.json
├── Repositorios/
│   ├── ReCliente/
│   │   ├── IReCliente.cs
│   │   └── ReClienteRam.cs
│   ├── ReEmpleado/
│   │   ├── IReEmpleado.cs
│   │   └── ReEmpleadoRam.cs
│   ├── ReProducto/
│   │   ├── IReProducto.cs
│   │   └── ReProductoRam.cs
│   ├── ReProveedor/
│   │   ├── IReProveedor.cs
│   │   └── ReProveedorRam.cs
│   └── ReVentas/
│       ├── IReVenta.cs
│       └── ReVentaRam.cs
├── Servicios/
│   ├── Jwt2/ 
│   │   ├── ServicioAutenticacion.cs
│   │   └── ServicioToken.cs      
│   ├── serviceCliente.cs
│   ├── serviceEmpleados.cs
│   ├── serviceProducto.cs
│   ├── serviceProovedor.cs
│   └── serviceVentas.cs
├── appsettings.Development.json
├── appsettings.json
├── Farmacia.csproj
├── Farmacia.http
├── Program.cs
└── README.md
```

---

## ⚙️ Requisitos Previos

- [.NET SDK](https://dotnet.microsoft.com/download) instalado.
- Motor de base de datos configurado según tu cadena de conexión en `appsettings.json`.
- [Postman](https://www.postman.com/downloads/) (opcional, para ejecutar las pruebas incluidas).

---

## ▶️ Instalación y Ejecución

1. **Clonar el repositorio:**
   ```bash
   git clone <URL_DE_TU_REPOSITORIO>
   cd <NOMBRE_DE_LA_CARPETA>
   ```

2. **Configurar variables y conexión a base de datos:**
   Verifica la cadena de conexión y la clave secreta de JWT en `appsettings.json`.

3. **Aplicar migraciones de Entity Framework Core:**
   ```bash
   dotnet ef database update
   ```

4. **Ejecutar la aplicación:**
   ```bash
   dotnet run
   ```

La API estará disponible por defecto en: `http://localhost:5124`

---

## 📡 Documentación de Endpoints

### 🔐 Autenticación (`/api/auth`)

| Método | Endpoint | Descripción | Autenticación |
| :--- | :--- | :--- | :---: |
| `POST` | `/api/auth/registro` | Registra un nuevo usuario | No |
| `POST` | `/api/auth/login` | Inicia sesión y devuelve un token JWT | No |
| `GET` | `/api/auth/yo` | Obtiene los datos del usuario autenticado | **Bearer Token** |

**Ejemplo de cuerpo (`POST /api/auth/login` y `registro`):**
```json
{
  "correo": "carlos@farmaco.com",
  "clave": "clave123"
}
```

---

### 👥 Clientes (`/api/Cliente`)

| Método | Endpoint | Descripción |
| :--- | :--- | :--- |
| `GET` | `/api/Cliente` | Lista todos los clientes |
| `GET` | `/api/Cliente/{dni}` | Busca un cliente por su DNI |
| `GET` | `/api/Cliente/frecuentes` | Lista los clientes frecuentes (por puntos) |
| `POST` | `/api/Cliente` | Registra un nuevo cliente |
| `PUT` | `/api/Cliente/{dni}` | Actualiza los datos de un cliente |
| `DELETE` | `/api/Cliente/{dni}` | Elimina un cliente por DNI |

**Ejemplo de cuerpo (`POST /api/Cliente`):**
```json
{
  "dni": "75889900",
  "nombre": "Andrés",
  "apellido": "Salazar",
  "correo": "andres.salazar@gmail.com",
  "telefono": "944556677",
  "puntosConseguidos": 50
}
```

---

### 🧑‍⚕️ Empleados (`/api/Empleados`)

| Método | Endpoint | Descripción |
| :--- | :--- | :--- |
| `GET` | `/api/Empleados` | Lista todos los empleados |
| `GET` | `/api/Empleados/{dni}` | Busca un empleado por su DNI |
| `GET` | `/api/Empleados/cargo/{cargo}` | Filtra empleados por cargo (ej. `FARMACEUTICO`, `CAJERO`, `ALMACENERO`) |
| `POST` | `/api/Empleados` | Registra un nuevo empleado |
| `PUT` | `/api/Empleados/{dni}` | Actualiza los datos de un empleado |
| `DELETE` | `/api/Empleados/{dni}` | Elimina un empleado por DNI |

**Ejemplo de cuerpo (`POST /api/Empleados`):**
```json
{
  "dni": "74859612",
  "nombre": "Diego",
  "apellido": "Ramírez",
  "correo": "diego.ramirez@farmaco.com",
  "telefono": "955667788",
  "cargo": "CAJERO"
}
```

---

### 💊 Productos (`/api/Producto`)

| Método | Endpoint | Descripción |
| :--- | :--- | :--- |
| `GET` | `/api/Producto` | Lista todos los productos |
| `GET` | `/api/Producto/{nombre}` | Busca un producto por su nombre |
| `GET` | `/api/producto/buscar` | Búsqueda con paginación, filtro y ordenamiento |
| `POST` | `/api/Producto` | Registra un nuevo producto |
| `PUT` | `/api/Producto/{nombre}` | Actualiza un producto existente |
| `DELETE` | `/api/Producto/{nombre}` | Elimina un producto por nombre |

**Parámetros de consulta para `/api/producto/buscar`:**
- `pagina`: Número de página (ej. `1`)
- `tamano`: Cantidad de registros por página (ej. `2`)
- `precioMinimo`: Filtro por precio mínimo (ej. `15`)
- `orden`: Campo de ordenamiento (ej. `precio`)

*Ejemplo:* `GET /api/producto/buscar?precioMinimo=15&orden=precio&pagina=1&tamano=2`

**Ejemplo de cuerpo (`POST /api/Producto`):**
```json
{
  "nombre": "Cloroformo",
  "precio": 2.50,
  "stock": 10
}
```

---

### 🚚 Proveedores (`/api/Proveedores`)

| Método | Endpoint | Descripción |
| :--- | :--- | :--- |
| `GET` | `/api/Proveedores` | Lista todos los proveedores |
| `GET` | `/api/Proveedores/{nombreProveedor}` | Busca un proveedor por nombre |
| `POST` | `/api/Proveedores` | Registra un nuevo proveedor |
| `PUT` | `/api/Proveedores/{nombreProveedor}` | Actualiza un proveedor existente |
| `DELETE` | `/api/Proveedores/{nombreProveedor}` | Elimina un proveedor por nombre |

**Ejemplo de cuerpo (`POST /api/Proveedores`):**
```json
{
  "nombreProveedor": "Distribuidora FarmaSur",
  "telefonoProveedor": "987654321",
  "direccionProveedor": "Av. Los Héroes 450, Lima"
}
```

---

### 🧾 Ventas (`/api/Ventas`)

| Método | Endpoint | Descripción |
| :--- | :--- | :--- |
| `GET` | `/api/ventas` | Lista todas las ventas registradas |
| `GET` | `/api/ventas/{id}` | Obtiene el detalle de una venta por ID |
| `POST` | `/api/Ventas` | Registra una nueva venta |
| `PUT` | `/api/Ventas/{id}` | Actualiza una venta existente |
| `DELETE` | `/api/Ventas/{id}` | Elimina una venta por ID |

**Ejemplo de cuerpo (`POST /api/Ventas`):**
```json
{
  "clienteDni": "72451234",
  "idVenta": 5,
  "nombreCliente": "Ana Torres",
  "nombreProducto": "Vitamina C",
  "cantidad": 2,
  "precio": 12.50,
  "metodoPago": "TARJETA"
}
```
*(Métodos de pago soportados en pruebas: `TARJETA`, `YAPE_PLIN`, etc.)*

---

## 🧪 Pruebas con Postman

En la carpeta `Postman/` encontrarás las colecciones listas para importar:

1. Abre **Postman** y haz clic en **Import**.
2. Selecciona los archivos `.json` ubicados en la carpeta `Postman/`.
3. Para probar rutas protegidas con JWT:
   - Ejecuta la petición **registro** y luego **login** en la colección `jwt.postman_collection.json`.
   - El script de pruebas en `login` guarda automáticamente el token en la variable de colección `{{token}}` para usarlo en el endpoint `/api/auth/yo`.
4. Las colecciones incluyen tanto peticiones válidas (CRUD) como casos de prueba negativos (datos duplicados, formatos inválidos, recursos inexistentes) para verificar las validaciones y el manejo de errores.