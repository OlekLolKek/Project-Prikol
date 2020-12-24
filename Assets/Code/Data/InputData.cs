using UnityEngine;

namespace ProjectPrikol
{
    [CreateAssetMenu(fileName = "InputData", menuName = "Data/InputData")]
    public class InputData : ScriptableObject, IData
    {
        [SerializeField] private KeyCode _crouch;
        [SerializeField] private KeyCode _jump;

        public KeyCode Crouch => _crouch;
        public KeyCode Jump => _jump;
    }
}