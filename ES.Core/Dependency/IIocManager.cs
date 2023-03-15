using System;
using Autofac;
using Autofac.Core;

namespace ES.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IIocManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        bool IsRegistered(Type serviceType, ILifetimeScope scope = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        object Resolve(Type type, ILifetimeScope scope = null);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        T Resolve<T>(string key = "", ILifetimeScope scope = null) where T : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        T Resolve<T>(params Parameter[] parameters) where T : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        T[] ResolveAll<T>(string key = "", ILifetimeScope scope = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        object ResolveOptional(Type serviceType, ILifetimeScope scope = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        object ResolveUnregistered(Type type, ILifetimeScope scope = null);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scope"></param>
        /// <returns></returns>
        T ResolveUnregistered<T>(ILifetimeScope scope = null) where T : class;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ILifetimeScope Scope();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="scope"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        bool TryResolve(Type serviceType, ILifetimeScope scope, out object instance);
    }
}