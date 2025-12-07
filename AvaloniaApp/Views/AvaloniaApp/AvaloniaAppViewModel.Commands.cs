using ReactiveUI;
using System;
using System.Reactive;
using System.Windows.Input;


namespace AvaloniaApp.Client.Views.AvaloniaApp
{
    public sealed partial class AvaloniaAppViewModel
    {
        public sealed class AvaloniaAppViewModelCommands
        {
            public AvaloniaAppViewModelCommands(AvaloniaAppViewModel vm)
            {
                // VoidMethod - синхронный, UI все-равно зависает
                VoidMethod = ReactiveCommand.Create(
                    () => vm.VoidMethod()
                );

                // FakeMethodAsync - тоже синхронный внутри
                FakeMethodAsync = ReactiveCommand.Create(
                    () => vm.FakeMethodAsync()
                );

                // Асинхронные команды 
                PseudoCpuBoundAsync = ReactiveCommand.CreateFromTask(
                    async () => await vm.PseudoCpuBoundAsync()
                );

                IoBoundAsync = ReactiveCommand.CreateFromTask(
                    async () => await vm.IoBoundAsync()
                );
            }

            public ICommand VoidMethod { get; }
            public ICommand FakeMethodAsync { get; }
            public ReactiveCommand<Unit, Unit> PseudoCpuBoundAsync { get; }
            public ReactiveCommand<Unit, Unit> IoBoundAsync { get; }

        }

        
        private AvaloniaAppViewModelCommands _commands;

        public AvaloniaAppViewModelCommands Commands => _commands ??= new(this);
    }
}
