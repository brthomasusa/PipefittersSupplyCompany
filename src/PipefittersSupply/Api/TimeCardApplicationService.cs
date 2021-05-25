using System;
using System.Threading.Tasks;
using PipefittersSupply.Domain.HumanResources.Employees;
using PipefittersSupply.Domain.HumanResources.TimeCards;
using PipefittersSupply.Domain.Repository;
using PipefittersSupply.Framework;
using static PipefittersSupply.Contracts.HumanResources.TimeCardCommand;

namespace PipefittersSupply.Api
{
    public class TimeCardApplicationService : IApplicationService
    {
        private readonly ITimeCardRepository _repo;

        public TimeCardApplicationService(ITimeCardRepository repo) => _repo = repo;

        public Task Handle(object command) =>
            command switch
            {
                V1.Create cmd =>
                    HandleCreate(cmd),
                V1.UpdateEmployeeId cmd =>
                    HandleUpdate(
                        cmd.Id,
                        emp => emp.UpdateEmployeeId(new EmployeeId(cmd.EmployeeId))
                    ),
                V1.UpdateSupervisorId cmd =>
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


        private async Task HandleCreate(V1.Create cmd)
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

            await _repo.Save(timeCard);
        }

        private async Task HandleUpdate(int timeCardID, Action<TimeCard> operation)
        {
            var timeCard = await _repo.Load(timeCardID.ToString());

            if (timeCard == null)
            {
                throw new InvalidOperationException($"Entity with id {timeCardID} could not be found!");
            }

            operation(timeCard);

            await _repo.Save(timeCard);
        }
    }
}