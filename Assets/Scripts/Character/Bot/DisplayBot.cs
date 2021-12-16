using UnityEngine;

public class DisplayBot : MonoBehaviour
{
    [System.Serializable]
    public struct BotDisplay
    {
        public Material botMaterial;
        public Sprite botAvatar;
    }

    public BotDisplay[] listBotDisplay;
    private SkinnedMeshRenderer skinMesh;
    private CharacterParams characterParams;
    private void Awake()
    {
        
    }
}
