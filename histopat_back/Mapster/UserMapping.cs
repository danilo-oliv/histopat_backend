using histopat_back.ViewModel.User;
using Mapster;
using histopat_back.Dominio.Models.User;


namespace Sistema.Universitario.Web.Mapster.Configs
{
    public class UserMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<User, UserGet>()
                  .Map(dest => dest.Roles, src => src.UserRoles.Select(ur => ur.Role).ToList());
        }
    }
}
