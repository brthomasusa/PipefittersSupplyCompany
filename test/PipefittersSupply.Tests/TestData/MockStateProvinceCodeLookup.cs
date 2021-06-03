using System.Collections.Generic;
using System.Linq;
using PipefittersSupply.Domain.Lookup;

namespace PipefittersSupply.Tests
{
    public class MockStateProvinceCodeLookup : IStateProvinceLookup
    {
        private readonly IEnumerable<StateCode> _stateCodes = new[]
        {
            new StateCode
            {
                StateCodeId = 1,
                StateProvinceCode = "AK",
                StateProvinceName = "Alaska"
            },
            new StateCode
            {
                StateCodeId = 2,
                StateProvinceCode = "AL",
                StateProvinceName = "Alabama"
            },
            new StateCode
            {
                StateCodeId = 3,
                StateProvinceCode = "CO",
                StateProvinceName = "Colorado"
            },
            new StateCode
            {
                StateCodeId = 4,
                StateProvinceCode = "FL",
                StateProvinceName = "Florida"
            },
            new StateCode
            {
                StateCodeId = 5,
                StateProvinceCode = "MN",
                StateProvinceName = "Minnesota"
            },
            new StateCode
            {
                StateCodeId = 6,
                StateProvinceCode = "NJ",
                StateProvinceName = "New Jersey"
            },
            new StateCode
            {
                StateCodeId = 7,
                StateProvinceCode = "SC",
                StateProvinceName = "South Carolina"
            },
                    new StateCode
            {
                StateCodeId = 8,
                StateProvinceCode = "TX",
                StateProvinceName = "Texas"
            }
        };
        public StateCode FindStateProvinceCode(string stateCode)
        {
            var statecode = _stateCodes.FirstOrDefault(sc => sc.StateProvinceCode == stateCode);
            return statecode ?? StateCode.None;
        }
    }
}