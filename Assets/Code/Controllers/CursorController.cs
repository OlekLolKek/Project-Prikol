using UnityEngine;


namespace ProjectPrikol
{
    public class CursorController : IInitializable
    {
        public void Initialize()
        {
            Lock();
        }

        private void Lock()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}