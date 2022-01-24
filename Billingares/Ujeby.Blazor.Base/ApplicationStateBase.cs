
namespace Ujeby.Blazor.Base
{
	public class ApplicationStateBase
	{
		public event Action OnChange;

		protected void NotifyStateChanged() => OnChange?.Invoke();
	}
}
