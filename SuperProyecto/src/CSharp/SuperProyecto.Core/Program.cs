/* using System;
using System.Reflection.Metadata;
using QRCoder;
using SkiaSharp;

 
dotnet add package QRCoder
dotnet add package SkiaSharp
dotnet add package SkiaSharp.NativeAssets.Linux.NoDependencies 


string url = "";

QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();

QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);

BitmapByteQRCode qRCode = new BitmapByteQRCode(qRCodeData);

byte[] qrCodeBytes = qRCode.GetGraphic(20);

using var bitmap = SKBitmap.Decode(qrCodeBytes);
using var image = SKImage.FromBitmap(bitmap);
using var data = image.Encode(SKEncodedImageFormat.Png, 100);
File.WriteAllBytes("qrCode.png", data.ToArray()); */

/* var Cliente = new List<Cliente>
{
    new Cliente { Nombre = "Juan", apellido = "Perez", DNI = 12345678, Telefono = 1122334455 },
    new Cliente { Nombre = "Maria", apellido = "Gomez", DNI = 87654321, Telefono = 5544332211 },
    new Cliente { Nombre = "Luis", apellido = "Lopez", DNI = 11223344, Telefono = 6677889900 }
};
app.MapGet("/Cliente/{DNI}", (int DNI) =>
{
    var cliiente = cliente.Where(s => s.DNI == DNI).ToList();
    if (!cliiente.Any()) return Results.NotFound("No se encontro el cliente");

    return Results.Ok(cliiente);
});



var Sector = new List<Sector>
{
    new Sector { IdSector = 1, sector = "Platea", IdLocal = 1 },
    new Sector { IdSector = 2, sector = "Popular", IdLocal = 3 },
    new Sector { IdSector = 3, sector = "Sector Vip", IdLocal = 2 }
};


// GET /locales/{localId}/sectores - Lista sectores del local
app.MapGet("/locales/{localId}/sectores", (int localId) =>
{
    var sectoresDelLocal = sectores.Where(s => s.IdLocal == localId).ToList();
    if (!sectoresDelLocal.Any()) return Results.NotFound("No se encontraron sectores para este local.");

    return Results.Ok(sectoresDelLocal);
});

// PUT /sectores/{sectorId} - Actualiza un sector
app.MapPut("/sectores/{sectorId}", (int sectorId, Sector sectorActualizado) =>
{
    var sector = sectores.FirstOrDefault(s => s.IdSector == sectorId);
    if (sector == null) return Results.NotFound("Sector no encontrado.");

    sector.sector = sectorActualizado.sector;
    return Results.NoContent();
});

// DELETE /sectores/{sectorId} - Elimina un sector
app.MapDelete("/sectores/{sectorId}", (int sectorId) =>
{
    var sector = sectores.FirstOrDefault(s => s.IdSector == sectorId);
    if (sector == null) return Results.NotFound("Sector no encontrado.");

    // Validar que no tenga tarifas/funciones asociadas (lógica pendiente)
    sectores.Remove(sector);
    return Results.NoContent();
});


app.Run();


var Local = new list<Local>
{
    new Local { IdLocal = 1, nombre = "La Rural", direccion = "Av. Sarmiento 2704", CapacidadMax = 5000 },
    new Local { IdLocal = 2, nombre = "Estadio Luna Park", direccion = "Av. Madero 470", CapacidadMax = 8000 },
    new Local {IdLocal=3 ,nombre="Movistar",direccion="casa de Celeste Zurita",capacidadMax=2500}
};
//- Lista de local
app.MapGet("/locales/{Idlocal}", (int IdLocal) =>
{
    var loocal = locales.Where(s => s.IdLocal == IdLocal).ToList();
    if (!loocal.Any()) return Results.NotFound("No se encontraron sectores para este local.");

    return Results.Ok(loocal);
});

// - Actualiza un local
app.MapPut("/Local/{IdLocal}", (int IdLocal, Local LocalActualizado) =>
{
    var local = locales.FirstOrDefault(s => s.IdLocal == LocalId);
    if (local == null) return Results.NotFound("local no encontrado.");

    Local.local = LocalActualizado.local;
    return Results.NoContent();
});

// - Elimina un local
app.MapDelete("/Local/{IdLocal}", (int LocalId) =>
{
    var local = locales.FirstOrDefault(s => s.IdLocal == LocalId);
    if (local == null) return Results.NotFound("Local no encontrado.");

    // Validar que no tenga tarifas/funciones asociadas (lógica pendiente)
    locales.Remove(local);
    return Results.NoContent();
});


app.mapget("/api/locales", () => "Hello World!");


app.mapget("/api/cliente", () => "Hello World!");
app.mapget("/api/evento", () => "Hello World!");
app.mapget("/api/tarifa", () => "Hello World!");
app.mapget("/api/sector", () => "Hello World!");
app.mapget("/api/funcion", () => "Hello World!");
app.mapget("/api/entrada", () => "Hello World!");
app.mapget("/api/orden", () => "Hello World!"); */