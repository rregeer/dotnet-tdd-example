using System;

namespace tddexample
{
	public class UnExpectedResultException: Exception
	{
		public UnExpectedResultException ()
		{
		}

		public UnExpectedResultException(string message)
			: base(message)
		{
		}

		public UnExpectedResultException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}