using AutoMapper;
using System;

namespace NinjectTest.AutoMapper
{
    public class LazyInitializedMappingEngine : IMappingEngine
    {
        public IConfigurationProvider ConfigurationProvider
        {
            get { return Mapper.Engine.ConfigurationProvider; }
        }

        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        public TDestination Map<TDestination>(object source, Action<IMappingOperationOptions> opts)
        {
            return Mapper.Map<TDestination>(source, opts);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions<TSource, TDestination>> opts)
        {
            return Mapper.Map<TSource, TDestination>(source, opts);
        }

        public TDestination Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions> opts)
        {
            return Mapper.Map<TSource, TDestination>(source, opts);
        }

        public void Dispose()
        {
            // todo: you should probably not do this but have AutoMapper control the disposing of the engine.
            Mapper.Engine.Dispose();
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMappingOperationOptions<TSource, TDestination>> opts)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMappingOperationOptions> opts)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
        {
            throw new NotImplementedException();
        }

        public TDestination DynamicMap<TSource, TDestination>(TSource source)
        {
            throw new NotImplementedException();
        }

        public TDestination DynamicMap<TDestination>(object source)
        {
            throw new NotImplementedException();
        }

        public object DynamicMap(object source, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }

        public void DynamicMap<TSource, TDestination>(TSource source, TDestination destination)
        {
            throw new NotImplementedException();
        }

        public void DynamicMap(object source, object destination, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }
    }
}