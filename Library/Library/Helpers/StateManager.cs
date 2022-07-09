using Library.Enums;

namespace Library.Helpers
{
    /// <summary>
    /// Change state for quitting or interacting with the application.
    /// </summary>
    internal static class StateManager
    {
        #region Properties
        private static int _state;
        #endregion

        #region Set State
        internal static void SetState(StateEnum newValue)
        {
            _state = (int)newValue;
        }
        #endregion

        #region Get State
        internal static int GetState()
        {
             return _state;
        }
        #endregion
    }
}
