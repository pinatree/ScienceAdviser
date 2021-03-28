using Autofac;

namespace ScienceAdviser.DI
{
    //Внедрение зависимостей в программе висит на этом небольшом статическом классе
    //Обратите внимание, что контейнер нужно инициализировать где-нибудь в другом месте!
    public static class DIContainer
    {
        //Контейнер
        public static IContainer Container;

        public static T GetInstance<T>()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                return scope.Resolve<T>();
            }
        }
    }
}
