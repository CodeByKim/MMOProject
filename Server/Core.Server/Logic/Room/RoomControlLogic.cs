using System;

namespace Core.Server
{
    internal class RoomControlLogic<TConnection> : AbstractSystemLogic<TConnection>
        where TConnection : ClientConnection<TConnection>, new()
    {
        private List<Room<TConnection>> _rooms;

        internal RoomControlLogic(AbstractServer<TConnection> server) : base(server)
        {
            _rooms = new List<Room<TConnection>>();
        }

        public override void OnInitialize()
        {
            var roomCount = ServerConfig.Instance.RoomCount;
            for (var i = 0; i < roomCount; i++)
            {
                var room = new Room<TConnection>();
                room.Initialize();

                _rooms.Add(room);
            }
        }

        public override void OnNewConnection(TConnection conn)
        {
            AddConnectionToRoom(conn);
        }

        public override void OnUpdate()
        {
            foreach (var room in _rooms)
                room.OnUpdate();
        }

        private void AddConnectionToRoom(TConnection conn)
        {
            /*
             * 가중치를 구해서 룸을 가져와야 하지만
             * 임시로 첫번째 꺼내온다.
             */
            var room = _rooms.First();
            room.Add(conn);
        }
    }
}
