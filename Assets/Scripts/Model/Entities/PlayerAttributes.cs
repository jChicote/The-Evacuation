namespace TheEvacuation.Model.Entities
{

    [System.Serializable]
    public class PlayerAttributes : BaseEntity
    {

        #region - - - - - - Properties - - - - - -

        public float Speed { get; set; }

        #endregion Properties

        #region - - - - - - Constructors - - - - - -

        public PlayerAttributes()
        {
            Speed = 0;
        }

        #endregion Constructors

    }

}