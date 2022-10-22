using Autofac;

namespace Game_Safe_of_the_pilot_brothers.Model
{
    internal class GameModule: Module
    {
        internal static IContainer Container { get; set; }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Lever>().As<ILever>();
            builder.RegisterType<Game>().AsSelf().As<IGameService>();
        }
    }
}
