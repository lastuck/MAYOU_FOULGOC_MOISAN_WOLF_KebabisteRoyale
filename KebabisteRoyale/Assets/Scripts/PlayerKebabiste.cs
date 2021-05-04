public class PlayerKebabiste : Kebabiste
{ 
    public PlayerInputs playerInputs;
    public bool isAI;

    public override KebabisteIntent GetIntent()
    {
        KebabisteIntent toReturn = playerInputs.intent;
        playerInputs.intent = null;
        return toReturn;
    }
}
