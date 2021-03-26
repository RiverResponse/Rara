namespace Messages
{
    public class ActivateUIMessage
    {
        /// <summary>
        /// Different app-states
        /// </summary>
        public enum AppStateTypes
        {
            None,
            LevelEditor,
            EntityEditor,
            Simulation
        }

        public AppStateTypes AppStateType;
        
        public ActivateUIMessage(AppStateTypes appStateType)
        {
            AppStateType = appStateType;
        }
    }
}