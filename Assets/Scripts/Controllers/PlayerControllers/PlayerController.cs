public abstract class PlayerController {

	public abstract void Horizontal(float axisValue);
	public abstract void Vertical(float axisValue);
	public abstract void Idle();
	public abstract void Action();
	public abstract void ActionRelease();
	public abstract void AltAction();
	public abstract void AltActionRelease();
	public abstract void Cancel();
	public abstract void CancelRelease();
	public abstract void Start();
	public abstract void StartRelease();
}
