using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookList.Core
{
    public interface ILoggerAdapter<T>
    {
        public interface ILoggerAdapter<T>
        {
            void LogError( string? message, [CallerMemberName] string memberName = "", params object?[] args);
            void LogError( string? message, Exception? exception, [CallerMemberName] string memberName = "", params object?[] args);

            void LogWarning(string message, [CallerMemberName] string memberName = "", params object?[] args);

            void LogCritical( string message, Exception? exception, [CallerMemberName] string memberName = "", params object?[] args);

            void LogInformation( string message, [CallerMemberName] string memberName = "", params object?[] args);
        }
    }
}
