using System.Linq.Expressions;
using System.Reflection;
using Humanizer;

namespace Domain.Common;

public static class RealtimeHelper
{
    public static class Groups
    {
        ///<summary> $"{typeof(T).Name}_{identifyingProperty}_{identifyingPropertyValue}_Updated" => User_Id_1_Updated, Entity_Id_1_Updated </summary>
        public static string EntityUpdated<T, U>(T entity, Expression<Func<T, U>> identifyingPropertyExpression)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (identifyingPropertyExpression == null)
            {
                throw new ArgumentNullException(nameof(identifyingPropertyExpression));
            }

            var memberExpression = identifyingPropertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("The identifying property expression must be a MemberExpression.", nameof(identifyingPropertyExpression));
            }

            var identifyingProperty = memberExpression.Member.Name;
            var identifyingPropertyValue = identifyingPropertyExpression.Compile()(entity);
            return $"{typeof(T).Name}_{identifyingProperty}_{identifyingPropertyValue}_Updated";
        }

        ///<summary> $"{typeof(T).Name.Pluralize()}Changed" => UsersChanged, EntitiesChanged </summary>
        public static string EntitiesUpdated<T>(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            return $"{typeof(T).Name.Pluralize()}Changed";
        }
    }

    public static class Messages
    {
        ///<summary> $"{typeof(T).Name}Inserted" => UserInserted, EntityInserted </summary>
        public static string EntityInserted<T>(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return $"{typeof(T).Name}Inserted";
        }

        ///<summary> $"{typeof(T).Name}Updated" => UserUpdated, EntityUpdated </summary>
        public static string EntityUpdated<T>(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return $"{typeof(T).Name}Updated";
        }

        ///<summary> $"{typeof(T).Name}Deleted" => UserDeleted, EntityDeleted </summary>
        public static string EntityDeleted<T>(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return $"{typeof(T).Name}Deleted";
        }

        ///<summary> $"{typeof(T).Name}_{fieldName}_{fieldValue}_changed" => User_FirstName_John_changed, Entity_Id_1_changed </summary>
        public static string EntityChanged<T, U>(T entity, Expression<Func<T, U>> field) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            if (!(field.Body is MemberExpression memberExpression))
            {
                throw new ArgumentException("The 'field' parameter must be a member expression.");
            }

            var fieldName = memberExpression.Member.Name;
            var fieldValue = GetFieldValue(field, entity);
            return $"{typeof(T).Name}_{fieldName}_{fieldValue}_changed";
        }

        ///<summary> $"{typeof(T).Name.Pluralize()}_{fieldName}_{fieldValue}_changed" => Users_FirstName_John_changed, Entities_Id_1_changed </summary>
        public static string EntitiesChanged<T, U>(IEnumerable<T> entities, Expression<Func<T, U>> field) where T : class
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            if (!(field.Body is MemberExpression memberExpression))
            {
                throw new ArgumentException("The 'field' parameter must be a member expression.");
            }

            var fieldName = memberExpression.Member.Name;
            var fieldValues = entities.Select(e => GetFieldValue(field, e));
            return $"{typeof(T).Name.Pluralize()}_{fieldName}_{string.Join("_", fieldValues)}_changed";
        }
    }

    ///<summary> $"{typeof(T).Name}_{fieldName}_{fieldValue}" => User_FirstName_John, Entity_Id_1 </summary>
    private static object? GetFieldValue<T, U>(Expression<Func<T, U>> field, T entity)
    {
        var memberExpression = (MemberExpression)field.Body;
        var propertyInfo = (PropertyInfo)memberExpression.Member;
        return propertyInfo.GetValue(entity);
    }
}
