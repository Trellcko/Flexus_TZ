using System;
using System.Collections.Generic;

namespace Trell.Flexus_TZ.Core.Pool
{
	public class Pool<T> where T : IPoolable
	{
		private List<T> _poolObjects = new List<T>();

		private Func<T> _factory;

		private Action<T> _takeHandler;
		private Action<T> _giveHandler;

		public Pool(Func<T> factory, Action<T> takeHandler, Action<T> giveHandler)
		{
			_poolObjects = new List<T>();
			_factory = factory;
			_takeHandler = takeHandler;
			_giveHandler = giveHandler;
		}

		public Pool(Func<T> factory, Action<T> takeHandler, Action<T> giveHandler, int count)
		{
			_poolObjects = new List<T>(10);
			_factory = factory;
			_takeHandler = takeHandler;
			_giveHandler = giveHandler;
			for (int i = 0; i < count; i++)
			{
				Take(_factory());
			}
		}

		public T Give()
		{
			T poolObject = _poolObjects.Count == 0 ? _factory() : _poolObjects[0];
			_poolObjects.Remove(poolObject);
			_giveHandler?.Invoke(poolObject);
			return poolObject;
		}

		public void Take(T poolObject)
		{
			_poolObjects.Add(poolObject);
			_takeHandler?.Invoke(poolObject);
		}
	}
}
