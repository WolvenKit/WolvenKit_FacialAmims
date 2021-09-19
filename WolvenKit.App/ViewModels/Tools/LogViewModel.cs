using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using WolvenKit.Common.Services;

namespace WolvenKit.ViewModels.Tools
{
    /// <summary>
    /// Implements the viewmodel that drives the log view.
    /// </summary>
    public class LogViewModel : ToolViewModel
    {
        #region Fields

        /// <summary>
        /// Identifies the <see ref="ContentId"/> of this tool window.
        /// </summary>
        public const string ToolContentId = "Log_Tool";

        /// <summary>
        /// Identifies the caption string used for this tool window.
        /// </summary>
        public const string ToolTitle = "Log";

        private readonly ILoggerService _loggerService;

        // private readonly ReadOnlyObservableCollection<LogEntry> _logEntries;
        // public ReadOnlyObservableCollection<LogEntry> LogEntries => _logEntries;

        [Reactive] public FlowDocument Document { get; set; } = new();

        #endregion Fields

        #region constructors

        public LogViewModel(
            ILoggerService loggerService
            ) : base(ToolTitle)
        {
            _loggerService = loggerService;

            SetupToolDefaults();

            //filter, sort and populate reactive list,
            // _loggerService.Connect() //connect to the cache
            //     .ObserveOn(RxApp.MainThreadScheduler)
            //     .Bind(out _logEntries)
            //     .Subscribe(OnNext);
        }



        #endregion constructors

        #region properties

        #endregion properties

        #region methods

        private void SetupToolDefaults() => ContentId = ToolContentId;
        #endregion methods
    }
}