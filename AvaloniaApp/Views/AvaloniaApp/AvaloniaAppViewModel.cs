using Avalonia.Threading;
using AvaloniaApp.UI;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AvaloniaApp.Client.Views.AvaloniaApp
{
    public sealed partial class AvaloniaAppViewModel : ReactiveViewModelBase
    {
        private double _progress1;
        private double _progress2;
        private double _progress3;
        private double _progress4;


        /// <summary>
        /// Свойство для ProgressBar c VoidMethod() 
        /// </summary>
        public double Progress1 
        { 
            get => _progress1;
            
            set => this.RaiseAndSetIfChanged(ref _progress1, value);
        }
        /// <summary>
        /// Свойство для ProgressBar c FakeMethodAsync() 
        /// </summary>
        public double Progress2
        {
            get => _progress2;

            set => this.RaiseAndSetIfChanged(ref _progress2, value);
        }
        /// <summary>
        /// Свойство для ProgressBar c CpuBoundAsync()
        /// </summary>
        public double Progress3
        {
            get => _progress3;

            set => this.RaiseAndSetIfChanged(ref _progress3, value);
        }
        /// <summary>
        /// Свойство для ProgressBar c IoBoundAsync() 
        /// </summary>
        public double Progress4
        {
            get => _progress4;

            set => this.RaiseAndSetIfChanged(ref _progress4, value);
        }

        public AvaloniaAppViewModel() {}

        /// <summary>
        /// СИНХРОННЫЙ МЕТОД (ПРОБЛЕМНЫЙ)
        /// Демонстрирует проблему блокировки UI-потока
        /// Thread.Sleep() останавливает весь UI на 30ms за итерацию
        /// Прогресс-бар обновится только после завершения метода
        /// Кнопка не блокируется - можно "спамить" вызовами
        /// </summary>
        public void VoidMethod() 
        {
            Progress1 = 0;

            for (int i = 0; i <= 100; i++) 
            {
                Progress1 = i;

                Thread.Sleep(30);
            }
        }
        /// <summary>
        /// ФЕЙКОВЫЙ ASYNC (АНТИПАТТЕРН)
        /// Метод объявлен как async, но не использует настоящую асинхронность
        /// Наличие ключевого слова async не делает код асинхронным автоматически
        /// Все равно блокирует UI, как и первый метод
        /// </summary>
        public async Task FakeMethodAsync() 
        {
            Progress2 = 0;

            for (int i = 0; i <= 100; i++)
            {
                Progress2 = i;

                Thread.Sleep(30);
            }
        }
        /// <summary>
        /// ДЕМОНСТРАЦИЯ: Фоновая задача с имитацией работы
        /// Task.Run() переносит работу в фоновый поток
        /// Thread.Sleep() имитирует обработку
        /// Dispatcher.UIThread.Post() безопасно обновляет UI из фона
        /// 
        /// Thread.Sleep() - это ожидание (I/O-bound), а не вычисления
        /// Для настоящих CPU-bound операций используются вычисления вместо Sleep
        /// </summary>
        public async Task PseudoCpuBoundAsync()
        {
            Progress3 = 0;

            await Task.Run(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    Thread.Sleep(30);   // Имитация работы/ожидания

                    Dispatcher.UIThread.Post(() => Progress3 = i);
                }
            });
        }
        /// <summary>
        /// I/O-BOUND АСИНХРОННАЯ ОПЕРАЦИЯ  
        /// Правильный способ выполнения операций ввода-вывода
        /// await Task.Delay() освобождает UI-поток на время ожидания
        /// Идеально для: чтение файлов, сетевые запросы, работа с БД
        /// </summary>
        public async Task IoBoundAsync() 
        {
            Progress4 = 0;

            for (int i = 0; i <= 100; i++)
            {
                Progress4 = i;

                await Task.Delay(30);
            }
        }
    }
}
