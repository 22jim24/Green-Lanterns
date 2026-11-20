# 💊 API de Gestión de Farmacia (`Farmacia`)

API RESTful desarrollada en **ASP.NET Core (.NET 10.0)** para la administración integral de una farmacia. El proyecto implementa una arquitectura separada en capas, almacenamiento en memoria (RAM), validaciones mediante *DataAnnotations*, uso de interfaces, excepciones personalizadas y un *Middleware* centralizado para el manejo de errores.

**Autores:** Jimmy y Andres

---

## 🏗️ Estructura del Proyecto

```text
Farmacia/
├── Controllers/
│   ├── ClienteControll.cs
│   ├── EmpleadosControll.cs
│   ├── ProductoControll.cs
│   ├── ProveedoresControll.cs
│   └── VentasControll.cs
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
│   └── ErrorResponse.cs
├── Excepciones/
│   ├── ConflictoException.cs
│   └── RecursoNoEncontradoException.cs
├── Middlewares/
│   └── ManejoErrores.cs
├── Modelos/
│   ├── Cliente.cs
│   ├── Empleados.cs
│   ├── Persona.cs
│   ├── Producto.cs
│   ├── Proovedor.cs
│   └── Ventas.cs
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
│   ├── serviceCliente.cs
│   ├── serviceEmpleados.cs
│   ├── serviceProducto.cs
│   ├── serviceProovedor.cs
│   └── serviceVentas.cs
├── appsettings.Development.json
├── appsettings.json
├── Cliente.postman_collection.json
├── Empleados.postman_collection.json
├── Farmacia.csproj
├── Farmacia.http
├── Producto.postman_collection.json
├── Program.cs
├── Proveedor.postman_collection.json
├── README.md
└── Ventassss1.postman_collection.json
```

---

## 🚀 Requisitos y Ejecución

1. Tener instalado el **SDK de .NET 10.0**.
2. Abrir una terminal en la carpeta raíz del proyecto (`Farmacia`).
3. Ejecutar los siguientes comandos:

```bash
dotnet restore
dotnet run
```

---

## 📌 Documentación de Endpoints y Ejemplos

### 1. Módulo de Ventas (`/api/ventas`)
Gestiona las transacciones de venta. Cada registro cuenta con un `IdVenta` único para permitir que un mismo cliente realice múltiples compras.

| Método | Endpoint | Descripción | Respuestas |
| :--- | :--- | :--- | :--- |
| `GET` | `/api/ventas` | Lista todas las ventas registradas. | `200 OK` |
| `GET` | `/api/ventas/{idVenta}` | Busca una venta específica por su ID. | `200 OK`, `404 Not Found` |
| `GET` | `/api/ventas/metodo-pago/{metodo}` | Filtra las ventas por método de pago (`EFECTIVO`, `TARJETA`, `YAPE_PLIN`, `TRANSFERENCIA`). | `200 OK` |
| `POST` | `/api/ventas` | Crea una nueva venta y calcula el total automáticamente. | `201 Created`, `400 Bad Request`, `409 Conflict` |
| `PUT` | `/api/ventas/{idVenta}` | Actualiza una venta existente y recalcula el total. | `200 OK`, `400 Bad Request`, `404 Not Found` |
| `DELETE` | `/api/ventas/{idVenta}` | Elimina una venta por su ID. | `200 OK`, `404 Not Found` |

#### Ejemplo: Crear Venta (`POST /api/ventas`)
```json
{
  "idVenta": 5,
  "nombreCliente": "Ana Torres",
  "nombreProducto": "Paracetamol 500mg",
  "cantidad": 2,
  "precio": 2.50,
  "metodoPago": "YAPE_PLIN"
}
```

#### Ejemplo: Actualizar Venta (`PUT /api/ventas/5`)
```json
{
  "nombreCliente": "Ana Torres",
  "nombreProducto": "Ibuprofeno 400mg",
  "cantidad": 3,
  "precio": 3.00,
  "metodoPago": "EFECTIVO"
}
```

---

### 2. Módulo de Clientes (`/api/cliente`)
Administra el registro de clientes de la farmacia (hereda de `Persona.cs`).

| Método | Endpoint | Descripción | Respuestas |
| :--- | :--- | :--- | :--- |
| `GET` | `/api/cliente` | Lista todos los clientes. | `200 OK` |
| `GET` | `/api/cliente/{identificador}` | Busca un cliente específico. | `200 OK`, `404 Not Found` |
| `POST` | `/api/cliente` | Registra un nuevo cliente. | `201 Created`, `400 Bad Request`, `409 Conflict` |
| `PUT` | `/api/cliente/{identificador}` | Actualiza los datos de un cliente existente. | `200 OK`, `400 Bad Request`, `404 Not Found` |
| `DELETE` | `/api/cliente/{identificador}` | Elimina un cliente del sistema. | `200 OK`, `404 Not Found` |

#### Ejemplo: Crear Cliente (`POST /api/cliente`)
```json
{
  "dni": "74859632",
  "nombre": "Carlos",
  "apellido": "Mendoza",
  "telefono": "987654321",
  "email": "carlos.mendoza@correo.com"
}
```

---

### 3. Módulo de Empleados (`/api/empleados`)
Gestiona el personal que labora en la farmacia (hereda de `Persona.cs`).

| Método | Endpoint | Descripción | Respuestas |
| :--- | :--- | :--- | :--- |
| `GET` | `/api/empleados` | Lista todos los empleados registrados. | `200 OK` |
| `GET` | `/api/empleados/{identificador}` | Busca un empleado específico. | `200 OK`, `404 Not Found` |
| `POST` | `/api/empleados` | Registra un nuevo empleado. | `201 Created`, `400 Bad Request`, `409 Conflict` |
| `PUT` | `/api/empleados/{identificador}` | Actualiza los datos de un empleado. | `200 OK`, `400 Bad Request`, `404 Not Found` |
| `DELETE` | `/api/empleados/{identificador}` | Elimina un empleado del registro. | `200 OK`, `404 Not Found` |

#### Ejemplo: Crear Empleado (`POST /api/empleados`)
```json
{
  "dni": "45612378",
  "nombre": "Lucía",
  "apellido": "Gómez",
  "telefono": "912345678",
  "cargo": "Químico Farmacéutico",
  "salario": 2800.00
}
```

---

### 4. Módulo de Productos (`/api/producto`)
Controla el inventario y catálogo de medicamentos disponibles.

| Método | Endpoint | Descripción | Respuestas |
| :--- | :--- | :--- | :--- |
| `GET` | `/api/producto` | Lista todos los productos del catálogo. | `200 OK` |
| `GET` | `/api/producto/{nombre}` | Busca un producto por su nombre o código. | `200 OK`, `404 Not Found` |
| `POST` | `/api/producto` | Agrega un nuevo producto al inventario. | `201 Created`, `400 Bad Request`, `409 Conflict` |
| `PUT` | `/api/producto/{nombre}` | Actualiza precio o stock de un producto. | `200 OK`, `400 Bad Request`, `404 Not Found` |
| `DELETE` | `/api/producto/{nombre}` | Elimina un producto del inventario. | `200 OK`, `404 Not Found` |

#### Ejemplo: Crear Producto (`POST /api/producto`)
```json
{
  "nombre": "Amoxicilina 500mg",
  "categoria": "Antibióticos",
  "precio": 8.50,
  "stock": 100
}
```

---

### 5. Módulo de Proveedores (`/api/proveedores`)
Gestiona los laboratorios y distribuidores que abastecen a la farmacia.

| Método | Endpoint | Descripción | Respuestas |
| :--- | :--- | :--- | :--- |
| `GET` | `/api/proveedores` | Lista todos los proveedores. | `200 OK` |
| `GET` | `/api/proveedores/{identificador}` | Busca un proveedor específico. | `200 OK`, `404 Not Found` |
| `POST` | `/api/proveedores` | Registra un nuevo proveedor. | `201 Created`, `400 Bad Request`, `409 Conflict` |
| `PUT` | `/api/proveedores/{identificador}` | Actualiza los datos de un proveedor. | `200 OK`, `400 Bad Request`, `404 Not Found` |
| `DELETE` | `/api/proveedores/{identificador}` | Elimina un proveedor del sistema. | `200 OK`, `404 Not Found` |

#### Ejemplo: Crear Proveedor (`POST /api/proveedores`)
```json
{
  "ruc": "20547896321",
  "nombreEmpresa": "Laboratorios Portugal",
  "telefono": "014587963",
  "direccion": "Av. Industrial 450, Lima"
}
```

---

## ⚠️ Manejo de Errores (`ManejoErrores.cs` y `ErrorResponse.cs`)

El middleware centralizado captura las excepciones lanzadas en la capa de servicios y devuelve un JSON estandarizado según el tipo de error:

* **`400 Bad Request` (Validaciones de DTO):**
```json
{
  "status": 400,
  "errors": {
    "MetodoPago": [
      "Método de pago no válido"
    ]
  }
}
```

* **`404 Not Found` (`RecursoNoEncontradoException`):**
```json
{
  "codigo": 404,
  "mensaje": "No existe una venta con el ID 99"
}
```

* **`409 Conflict` (`ConflictoException`):**
```json
{
  "codigo": 409,
  "mensaje": "Ya existe una venta registrada con el ID 1"
}
```

---

## 🧪 Colecciones de Postman

El proyecto incluye 5 colecciones en la raíz con casos de prueba de éxito y de error, además de scripts de validación automática (*Tests*):

* `Cliente.postman_collection.json`
* `Empleados.postman_collection.json`
* `Producto.postman_collection.json`
* `Proveedor.postman_collection.json`
* `Ventassss1.postman_collection.json`