namespace Individuals.Domain.Entities
{
    public class City:Entity
    {
        #region Constructors

        protected City()
        {

        }
        public City(string name)
        {
            Name = name;
        }
        #endregion

        #region Properties

        public string Name { get; protected set; }

        #endregion

    }
}
