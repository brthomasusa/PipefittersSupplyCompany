using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.Lookup
{
    public interface IStateProvinceLookup
    {
        StateCode FindStateProvinceCode(string stateCode);
    }

    public class StateCode : Value<StateCode>
    {
        public int StateCodeId { get; set; }
        public string StateProvinceCode { get; set; }
        public string StateProvinceName { get; set; }

        public static StateCode None = new StateCode { StateCodeId = -1, StateProvinceCode = null, StateProvinceName = null };
    }
}