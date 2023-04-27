using System.Diagnostics.CodeAnalysis;

namespace Metimos
{
	public abstract class TextWrapper<T>
	{
		public void Parse(T condition, string format)
		{
			if (Condition(condition))
				Callback(format);
		}
		
		public void Parse<T1>(T condition, string format, T1 arg1)
		{
			if (Condition(condition))
				Callback(format, arg1);
		}

		public void Parse<T1, T2>(T condition, string format, T1 arg1, T2 arg2)
		{
			if (Condition(condition))
				Callback(format, arg1, arg2);
		}

		public void Parse<T1, T2, T3>(T condition, string format, T1 arg1, T2 arg2, T3 arg3)
		{
			if (Condition(condition))
				Callback(format, arg1, arg2, arg3);
		}
		
		public void Parse<T1, T2, T3, T4>(T condition, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			if (Condition(condition))
				Callback(format, arg1, arg2, arg3, arg4);
		}

		public void Parse<T1, T2, T3, T4, T5>(T condition, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
		{
			if (Condition(condition))
				Callback(format, arg1, arg2, arg3, arg4, arg5);
		}

		public void Parse<T1, T2, T3, T4, T5, T6>(T condition, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
		{
			if (Condition(condition))
				Callback(format, arg1, arg2, arg3, arg4, arg5, arg6);
		}

		public void Parse<T1, T2, T3, T4, T5, T6, T7>(T condition, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
		{
			if (Condition(condition))
				Callback(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
		}

		public void Parse<T1, T2, T3, T4, T5, T6, T7, T8>(T condition, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
		{
			if (Condition(condition))
				Callback(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
		}

		protected abstract bool Condition(T value);

		protected abstract void Callback(string format, params object[] args);
	}
}