using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.DTO;
using SuperProyecto.Services.Validators;
using MySqlConnector;

namespace SuperProyecto.Services.Service;

public class TarifaService : ITarifaService
{
    readonly IRepoTarifa _repoTarifa;
    readonly TarifaValidator _validador;
    public TarifaService(IRepoTarifa repoTarifa, TarifaValidator validador)
    {
        _repoTarifa = repoTarifa;
        _validador = validador;
    }

    public Result<IEnumerable<Tarifa>> GetTarifas()
    {
        try
        {
            return Result<IEnumerable<Tarifa>>.Ok(_repoTarifa.GetTarifas());
        }catch(MySqlException)
        {
            return Result<IEnumerable<Tarifa>>.Unauthorized();
        }
    }
    

    public Result<Tarifa?> DetalleTarifa(int id)
    {
        try
        {
            if (_repoTarifa.DetalleTarifa(id) is null) return Result<Tarifa?>.NotFound("La tarifa solicitada no fue encontrada.");
            return Result<Tarifa?>.Ok(_repoTarifa.DetalleTarifa(id));
        }catch(MySqlException)
        {
            return Result<Tarifa>.Unauthorized();
        }
    }

    public Result<TarifaDto> AltaTarifa(TarifaDto tarifaDto)
    {
        try
        {
            var resultado = _validador.Validate(tarifaDto);
            if (!resultado.IsValid)
            {
                var listaErrores = resultado.Errors
                    .GroupBy(a => a.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );
                return Result<TarifaDto>.BadRequest(listaErrores);
            }
            var tarifa = ConvertirDtoClase(tarifaDto);
            _repoTarifa.AltaTarifa(tarifa);
            return Result<TarifaDto>.Ok(tarifaDto);
        }catch(MySqlException)
        {
            return Result<TarifaDto>.Unauthorized();
        }
    }

    public Result<TarifaDto> UpdateTarifa(TarifaDto tarifaDto, int id)
    {
        try
        {
            var resultado = _validador.Validate(tarifaDto);
            if (!resultado.IsValid)
            {
                var listaErrores = resultado.Errors
                    .GroupBy(a => a.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );
                return Result<TarifaDto>.BadRequest(listaErrores);
            }
            var tarifa = ConvertirDtoClase(tarifaDto);
            _repoTarifa.UpdateTarifa(tarifa, id);
            return Result<TarifaDto>.Ok(tarifaDto);
        }catch(MySqlException)
        {
            return Result<TarifaDto>.Unauthorized();
        }
    }


    static Tarifa ConvertirDtoClase(TarifaDto tarifaDto)
    {
        return new Tarifa
        {
            idFuncion = tarifaDto.idFuncion,
            idSector = tarifaDto.idSector,
            precio = tarifaDto.precio,
            stock = tarifaDto.stock
        };
    }
}