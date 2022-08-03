namespace BlApi
{
    /// <summary>
    /// Class the return Instance of IBL
    /// </summary>
    public class BlFactory
    {
        /// <summary>
        /// Return he instanse of the BL.
        /// </summary>
        /// <return>The BL instance.</returns>
        public static IBL GetBl()
        {
            return BO.BL.Instance;
        }
    }
}
