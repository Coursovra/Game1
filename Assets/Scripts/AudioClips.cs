using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "AudioClips", menuName = "AudioClips", order = 0)]
    public class AudioClips : ScriptableObject
    {
        public AudioClip VictoryMusic;
        public AudioClip ScaryMusic;
        public AudioClip ActionMusic;
        public AudioClip RightHandAttack;
        public AudioClip LeftHandAttack;
    }
}