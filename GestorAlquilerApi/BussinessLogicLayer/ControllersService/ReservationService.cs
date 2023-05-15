using AutoMapper;
using GestorAlquilerApi.BussinessLogicLayer.Interfaces;
using GestorAlquilerApi.BussinessLogicLayer.Models;
using GestorAlquilerApi.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorAlquilerApi.BussinessLogicLayer.ControllersService
{
    public class ReservationService<ReservationDTO>
        : ControllerBase,
            IGenericService<ReservationDTO>
    {
        private readonly IQueryReservation _repository;
        private readonly IMapper _mapper;
        private readonly ISaveData<Reservation> _saveData;
        private readonly DbSet<Reservation> _reservations;

        //
        private readonly IQueryBranch _brachesRepo;
        private readonly DbSet<Branch> _branches;

        //
        private readonly IQueryClient _clientRepo;
        private readonly DbSet<Client> _clients;

        //
        private readonly IQueryCar _carsRepo;
        private readonly DbSet<Car> _cars;

        public ReservationService(
            IQueryReservation repository,
            IMapper mapper,
            ISaveData<Reservation> saveData,
            IQueryBranch brachesRepo,
            IQueryClient clientRepo,
            IQueryCar carsRepo
        )
        {
            _repository = repository;
            _mapper = mapper;
            _saveData = saveData;
            //
            _reservations = _repository.GetDataReservation();
            //
            _brachesRepo = brachesRepo;
            _branches = _brachesRepo.GetDataBranches();
            //
            _clientRepo = clientRepo;
            _clients = _clientRepo.GetDataClients();
            //
            _carsRepo = carsRepo;
            _cars = _carsRepo.GetDataCars();
        }

        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAllElements()
        {
            if (_reservations == null)
            {
                return NotFound();
            }

            if (!Convert.ToBoolean(_reservations.Count()))
                return Problem("There are no Reservations", statusCode: 404);

            var reservations = _reservations.Select(r => _mapper.Map<ReservationDTO>(r));

            return await reservations.ToListAsync();
        }

        public async Task<ActionResult<ReservationDTO>> GetElementById(int id)
        {
            if (_reservations == null)
            {
                return NotFound();
            }
            var reservation = await _reservations.FindAsync(id);

            if (reservation == null)
            {
                return Problem($"There are no Reservation with id:{id}");
            }
            var reservationDTO = _mapper.Map<ReservationDTO>(reservation);

            return reservationDTO;
        }

        public async Task<IActionResult> EditElement(int id, ReservationDTO reservationDTO)
        {
            var reservation = _mapper.Map<Reservation>(reservationDTO);

            if (id != reservation.Id)
            {
                return BadRequest();
            }

            _saveData.ModifiedState(reservation);

            try
            {
                await _saveData.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        public async Task<ActionResult<ReservationDTO>> AddElement(ReservationDTO reservationDTO)
        {
            if (_reservations == null)
            {
                return Problem("Entity set 'ApiContext.Reservation'  is null.");
            }

            var reservation = _mapper.Map<Reservation>(reservationDTO);

            //Validate DTO
            var valuesAsArray = Enum.GetNames(typeof(Car.Categories));
            if (!valuesAsArray.Contains(reservation.CarCategory))
            {
                return BadRequest(
                    $"Category '{reservation.CarCategory}' is invalid. It has to be in: {string.Join(", ", valuesAsArray.SkipLast(1))} or {valuesAsArray[^1]}"
                );
            }
            //Validate branchId
            else if (await _branches.FindAsync(reservation.BranchId) == null)
            {
                return BadRequest($"there is no branch with id = {reservation.BranchId}");
            }
            //Validate clientId
            else if (await _clients.FindAsync(reservation.ClientId) == null)
            {
                return BadRequest($"there is no customer with id = {reservation.ClientId}");
            }
            //Validate Dates
            else if (reservation.StartDate < DateTime.Now)
            {
                return BadRequest($"The StartDate must be greater than {DateTime.Now}.");
            }
            else if (reservation.StartDate > reservation.EndDate)
            {
                return BadRequest($"The End Date must be greater than Start Date.");
            }
            else if (reservation.EndDate < reservation.StartDate)
            {
                return BadRequest($"The End Date must be greater than start Date.");
            }
            else
            {
                if (AreCarsAvailables(reservation))
                {
                    try
                    {
                        RemoveCarFromAvailable(reservation, "remove");
                        _repository.AddReservation(reservation);
                        await _saveData.SaveChangesAsync();
                        return reservationDTO;
                    }
                    catch
                    {
                        return Problem("problem in adding reservation");
                    }
                }
                else
                {
                    return BadRequest("There are no available cars");
                }
            }
        }

        public async Task<IActionResult> RemoveElement(int id)
        {
            if (_reservations == null)
            {
                return NotFound();
            }
            var reservation = await _reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            RemoveCarFromAvailable(reservation, "add");
            _reservations.Remove(reservation);
            await _saveData.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservationExists(int id)
        {
            return (_reservations?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async void RemoveCarFromAvailable(Reservation reservation, string operation)
        {
            if (reservation.BranchId == reservation.ReturnBranchId)
            {
                var planning = _repository.GetReservationData(reservation);

                foreach (var day in planning)
                {
                    if (operation == "remove")
                    {
                        day.CarsAvailables--;
                    }
                    else
                    {
                        day.CarsAvailables++;
                    }
                }
            }
            else
            {
                //Aqui establece el id del coche con la sucursal nueva y añade disponiblidad
                //Obtener el coche
                var car = await _cars.FindAsync(reservation.CarId);
                //cambio de la branchId del coche
                if (car != null)
                {
                    car.BranchId = reservation.ReturnBranchId;
                }
                //quitar de la antigua desde el dia de reserva hasta el final
                var planningBranch = _repository.GetReservationDataBranch(reservation);

                foreach (var day in planningBranch)
                {
                    if (operation == "remove")
                    {
                        day.CarsAvailables--;
                    }
                    else
                    {
                        day.CarsAvailables++;
                    }
                }

                //añadir al planning de la nueva +1 de disponibilidad has ta el final
                var planningReturnBranch = _repository.GetReservationDataReturn(reservation);

                foreach (var day in planningReturnBranch)
                {
                    if (operation == "remove")
                    {
                        day.CarsAvailables++;
                    }
                    else
                    {
                        day.CarsAvailables--;
                    }
                }
            }
            await _saveData.SaveChangesAsync();
        }

        private bool AreCarsAvailables(Reservation reservation)
        {
            return _repository.CheckAvailabilityCars(reservation);
        }
    }
}
