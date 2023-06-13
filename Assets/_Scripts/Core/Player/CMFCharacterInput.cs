using CMF;

public class CMFCharacterInput : CharacterInput
{
    private Character _character;
    public Character Character
    {
        get
        {
            if (_character == null)
                _character = GetComponentInParent<Character>();
            
            return _character;
        }
    }
    
    public override float GetHorizontalMovementInput()
    {
        return Character.Player.PlayerInputReader.PlayerActions.Main.Horizontal.ReadValue<float>();
    }

    public override float GetVerticalMovementInput()
    {
        return Character.Player.PlayerInputReader.PlayerActions.Main.Vertical.ReadValue<float>();
    }

    public override bool IsJumpKeyPressed()
    {
        return Character.Player.PlayerInputReader.PlayerActions.Main.Jump.IsPressed();
    }
}