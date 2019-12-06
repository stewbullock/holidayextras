using ASM.Core.Serialisation;
using Ninject.Modules;

namespace ASM.Core.Dependency
{
    public class DependencyModule : NinjectModule, IDependencyModule
    {
        #region Methods 

        #region Constructor 

        private DependencyModule()
        { }

        #endregion

        public static INinjectModule Create()
        {
            return new DependencyModule();
        }

        public override void Load()
        {
            BindReaders();
            BindSerialisation();
        }

        private void BindSerialisation()
        {
            Bind<ISerializer>().To<Serializer>();
            Bind<IDeSerializer>().To<DeSerializer>();
        }

        private void BindReaders()
        {
            Bind<IFileReader>().To<FileReader>();
        }

        #endregion
    }
}