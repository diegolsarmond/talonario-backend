using System.ComponentModel;

namespace Talonario.Api.Server.Application.Enums
{
    public enum OrgaoAutuadorEnum
    {
        [Description("Geral")]
        R = 1,

        [Description("Estadual")]
        E = 2,

        [Description("Municipal")]
        M = 3
    }
}