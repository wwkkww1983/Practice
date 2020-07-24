using Caist.ICL.Core.Dao;
using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.ICL.Data
{
    public class Snapshot<T> : ISnapshot<T>
    {
        IDatabase database;
        PocoData poco;
        T tackedObject;
        Dictionary<PocoColumn, object> originalValues = new Dictionary<PocoColumn, object>();
        static IColumnSerializer serializer = new FastJsonColumnSerializer();
        public Snapshot(IDatabase database, T oldEntity)
        {
            this.database = database;
            poco = database.PocoDataFactory.ForType(typeof(T));
            tackedObject = oldEntity;

            var clone = tackedObject.Copy();
            foreach (var pcocColumn in poco.Columns.Values)
                originalValues[pcocColumn] = pcocColumn.GetColumnValue(poco, clone);
        }

        public void OverrideTrackedObject(T newEntity)
        {
            tackedObject = newEntity;
        }

        public IEnumerable<string> UpdatedColumns()
        {
            var list = new List<string>();

            foreach (var pcocColumn in originalValues)
            {
                var newValue = pcocColumn.Key.GetColumnValue(poco, tackedObject);

                if (newValue == null)
                {
                    //现有值如果为空，不用更新
                    continue;
                }

                var type = newValue.GetType();
                if (type.IsAClass() || type.IsArray)
                {
                    if (serializer.Serialize(newValue) == serializer.Serialize(pcocColumn.Value))
                    {
                        //序列化的值相同，不用更新
                        continue;
                    }
                }

                if (newValue.Equals(pcocColumn.Value))
                {
                    //值相同，不用更新
                    continue;
                }

                list.Add(pcocColumn.Key.ColumnName);
            }

            return list;
        }

        public void Update(T newEntity)
        {
            tackedObject = newEntity;
            database.Update(newEntity, UpdatedColumns());
        }
    }
}
