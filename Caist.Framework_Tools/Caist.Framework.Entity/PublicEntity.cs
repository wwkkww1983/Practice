using Caist.Framework.Entity.Entity;
using System.Collections.Generic;

namespace Caist.Framework.Entity
{
    public class PublicEntity
    {
        private static DeviceEntity device = new DeviceEntity();
        public static DeviceEntity Device { get => device; set => device = value; }

        private static List<DeviceEntity> deviceEntities = new List<DeviceEntity>();

        public static List<DeviceEntity> DeviceEntities { get => deviceEntities; set => deviceEntities = value; }

        private static TagGroupsEntity tagGroups = new TagGroupsEntity();
        public static TagGroupsEntity TagGroups { get => tagGroups; set => tagGroups = value; }

        private static List<TagGroupsEntity> tagGroupsEntities = new List<TagGroupsEntity>();
        public static List<TagGroupsEntity> TagGroupsEntities { get => tagGroupsEntities; set => tagGroupsEntities = value; }

        private static TagEntity tagEntity = new TagEntity();
        public static TagEntity TagEntity { get => tagEntity; set => tagEntity = value; }


        private static List<TagEntity> tagEntities = new List<TagEntity>();
        public static List<TagEntity> TagEntities { get => tagEntities; set => tagEntities = value; }

        private static List<AlarmEntity> alarmEntities = new List<AlarmEntity>();
        public static List<AlarmEntity> AlarmEntities { get => alarmEntities; set => alarmEntities = value; }

        private static List<FiberEntity> fiberEntities = new List<FiberEntity>();
        public static List<FiberEntity> FiberEntities { get => fiberEntities; set => fiberEntities = value; }

    }
}
