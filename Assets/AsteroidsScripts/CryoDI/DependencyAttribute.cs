using System;
using UnityEngine;

namespace CryoDI
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class DependencyAttribute : Attribute
	{
		public DependencyAttribute() {}

		public DependencyAttribute(string name)
		{
			Name = name;
		}

		public string Name { get; private set; }
	}
	
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class HardDependencyAttribute : Attribute
	{
		public HardDependencyAttribute(Type name)
		{
			Name = name;
		}
		
		public Type Name { get; private set; }

	}
}
