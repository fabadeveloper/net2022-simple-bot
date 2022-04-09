using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SimpleBotCore.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SimpleBotCore.Repositories
{
	public class UserProfileRepository : IUserProfileRepository
	{
        public readonly IMongoClient client;

        public UserProfileRepository(IMongoClient _client)
        {
            client = _client;
        }

        public SimpleUser TryLoadUser(string userId)
        {
            if (Exists(userId))
            {
                return GetUser(userId);
            }

            return null;
        }

        public SimpleUser Create(SimpleUser user)
        {
            if (Exists(user.id))
                throw new InvalidOperationException("Usuário ja existente");

            SaveUser(user);

            return user;
        }

        public void AtualizaNome(string userId, string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (!Exists(userId))
                throw new InvalidOperationException("Usuário não existe");

            var user = GetUser(userId);

            user.Nome = name;

            SaveUser(user);
        }

        public void AtualizaIdade(string userId, int idade)
        {
            if (idade <= 0)
                throw new ArgumentOutOfRangeException(nameof(idade));

            if (!Exists(userId))
                throw new InvalidOperationException("Usuário não existe");

            var user = GetUser(userId);

            user.Idade = idade;

            SaveUser(user);
        }

        public void AtualizaCor(string userId, string cor)
        {
            if (cor == null)
                throw new ArgumentNullException(nameof(cor));

            if (!Exists(userId))
                throw new InvalidOperationException("Usuário não existe");

            var user = GetUser(userId);

            user.Cor = cor;

            SaveUser(user);
        }

        private bool Exists(string userId)
        {
            if (GetUser(userId) is null)
                return false;
            else
                return true;
        }

        private SimpleUser GetUser(string userId)
        {
            var database = client.GetDatabase("BotEmulator");
            IMongoCollection<SimpleUser> colSimpleUser = database.GetCollection<SimpleUser>("SimpleUser");

            var filter = Builders<SimpleUser>.Filter.Where(m=> m.Codigo == userId);
            var simpleUser = colSimpleUser.Find(filter).FirstOrDefault();
            
            if (simpleUser != null)
            {
                return simpleUser;
            }

            return null;
        }

        private void SaveUser(SimpleUser user)
        {
            var database = client.GetDatabase("BotEmulator");
            IMongoCollection<SimpleUser> colSimpleUser = database.GetCollection<SimpleUser>("SimpleUser");

            var filter = Builders<SimpleUser>.Filter.Where(m => m.Codigo == user.Codigo);
            var simpleUser = colSimpleUser.Find(filter).FirstOrDefault();

            if (simpleUser != null)
            {
                ReplaceOneResult result = colSimpleUser.ReplaceOne(filter, user);
            }
            else
            {
                var data = database.GetCollection<SimpleUser>("SimpleUser");
                data.InsertOne(user);
            }
        }
        	}
}