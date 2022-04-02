namespace TheEvacuation.Model.Entities
{

    public class Player : BaseEntity
    {

        #region - - - - - - Properties - - - - - -

        public string Name { get; set; }

        public PlayerAttributes PlayerAttributes { get; set; } // Remove Later

        #endregion Properties

    }

}
