﻿using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using Ujeby.Blazor.Base.ViewModels;

namespace Ujeby.Blazor.Base.Components
{
	public class ComponentBase<TViewModel, TAppState> : Microsoft.AspNetCore.Components.ComponentBase, IDisposable, IAsyncDisposable
		 where TViewModel : ViewModelBase, new()
	{
		public TViewModel ViewModel { get; set; }

		public bool IsBusy { get; set; } = false;

		protected bool _disposed;

		[Inject]
		protected TAppState AppState { get; set; }

		public ComponentBase()
		{
			ViewModel = new();
		}

		protected override async Task OnInitializedAsync()
		{
			await OnLoadDataAsync();
		}

		protected override async Task OnParametersSetAsync()
		{
			await OnUpdateAsync();
		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
            if (firstRender)
                ViewModel.PropertyChanged += ViewModelPropertyChanged;

            await base.OnAfterRenderAsync(firstRender);
        }

		protected virtual async Task OnPropertyChangedAsync(string propertyName)
		{
			await OnUpdateAsync();
		}

        private async void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is TViewModel viewModel)
				await OnPropertyChangedAsync(e.PropertyName);

			StateHasChanged();
		}

		protected virtual async Task OnLoadDataAsync()
		{
			await Task.CompletedTask;
		}

		protected virtual async Task OnUpdateAsync()
		{
			await Task.CompletedTask;
		}

		/// <summary>
		/// https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose#implement-the-dispose-pattern
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Implementation of the Dispose pattern.
		/// https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose#implement-the-dispose-pattern
		/// </summary>
		/// <param name="disposing">if true then disposing of managed resources</param>
		protected virtual void Dispose(bool disposing)
		{
			// nothing to do
		}

		/// <summary>
		/// https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-disposeasync#implement-the-async-dispose-pattern
		/// </summary>
		/// <returns></returns>
		public async ValueTask DisposeAsync()
		{
			// Perform async cleanup.
			await DisposeAsyncCore();

			// Dispose of unmanaged resources.
			Dispose(false);

#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
			// Suppress finalization.
			GC.SuppressFinalize(this);
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
		}

		/// <summary>
		/// Implementation of the Async Dispose pattern.
		/// https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-disposeasync#implement-the-async-dispose-pattern
		/// </summary>
		/// <returns></returns>
		protected virtual ValueTask DisposeAsyncCore()
		{
			return ValueTask.CompletedTask;
		}
	}
}
