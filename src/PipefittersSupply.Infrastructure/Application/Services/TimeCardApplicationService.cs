using System;
using System.Threading.Tasks;
using PipefittersSupply.Domain.HumanResources.Employees;
using PipefittersSupply.Domain.HumanResources.TimeCards;
using PipefittersSupply.Domain.Interfaces;
using PipefittersSupply.Domain.Repository;
using static PipefittersSupply.Infrastructure.Application.Commands.HumanResources.TimeCardCommand;

namespace PipefittersSupply.Infrastructure.Application.Services
{
    public class TimeCardApplicationService : IApplicationService
    {
        private readonly ITimeCardRepository _repo;
        private readonly IUnitOfWork _unitOfWork;

        public TimeCardApplicationService(ITimeCardRepository repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(object command) =>
            command switch
            {
                V1.CreateTimeCard cmd =>
                    HandleCreate(cmd),
                V1.UpdateEmployeeId cmd =>
                    HandleUpdate(
                        cmd.Id,
                        emp => emp.UpdateEmployeeId(new EmployeeId(cmd.EmployeeId))
                    ),
                V1.UpdatTimeCardSupervisorId cmd =>
                    HandleUpdate(
                        cmd.Id,
                        emp => emp.UpdateSupervisorId(new EmployeeId(cmd.SupervisorId))
                    ),
                V1.UpdatePayPeriodEndDate cmd =>
                    HandleUpdate(
                        cmd.Id,
                        emp => emp.UpdatePayPeriodEnded(PayPeriodEndDate.FromDateTime(cmd.PayPeriodEnded))
                    ),
                V1.UpdateRegularHours cmd =>
                    HandleUpdate(
                        cmd.Id,
                        emp => emp.UpdateRegularHours(RegularHours.FromInterger(cmd.RegularHours))
                    ),
                V1.UpdateOvertimeHours cmd =>
                    HandleUpdate(
                        cmd.Id,
                        emp => emp.UpdateOvertimeHours(OvertimeHours.FromInterger(cmd.OvertimeHours))
                    ),
                _ => Task.CompletedTask
            };


        private async Task HandleCreate(V1.CreateTimeCard cmd)
        {
            if (await _repo.Exists(cmd.Id.ToString()))
            {
                throw new InvalidOperationException($"Entity with Id {cmd.Id} already exists!");
            }

            var timeCard = new TimeCard
            (
                new TimeCardId(cmd.Id),
                new EmployeeId(cmd.EmployeeId),
                new EmployeeId(cmd.SupervisorId),
                PayPeriodEndDate.FromDateTime(cmd.PayPeriodEnded),
                RegularHours.FromInterger(cmd.RegularHours),
                OvertimeHours.FromInterger(cmd.OvertimeHours)
            );

            await _repo.Add(timeCard);
            await _unitOfWork.Commit();
        }

        private async Task HandleUpdate(int timeCardID, Action<TimeCard> operation)
        {
            var timeCard = await _repo.Load(timeCardID.ToString());

            if (timeCard == null)
            {
                throw new InvalidOperationException($"Entity with id {timeCardID} could not be found!");
            }

            operation(timeCard);

            await _unitOfWork.Commit();
        }
    }
}