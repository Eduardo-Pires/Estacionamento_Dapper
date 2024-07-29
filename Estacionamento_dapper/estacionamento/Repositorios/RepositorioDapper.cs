using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using Mysqlx.Crud;

namespace estacionamento.Repositorios
{
    public class RepositorioDapper<T> : IRepositorio<T>
    {
        private readonly IDbConnection _conexao;
        private readonly string _nomeTabela;

        public RepositorioDapper(IDbConnection conexao)
        {
            _conexao = conexao;
            _nomeTabela = ObterNomeTabela();
        }

        private string ObterNomeTabela()
        {
            var tipo = typeof(T);
            var atributo = tipo.GetCustomAttribute<TableAttribute>();

            if (atributo != null)
            {
               return atributo.Name;
            }

            return tipo.Name;
        }

        private string ObterCamposInsert(T entidade)
        {
            var tipo = typeof(T);
            var propriedades = tipo.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => !Attribute.IsDefined(p, typeof(IgnoreDapperAttribute)));
            var nomesCampos = propriedades.Select(p =>{
                var columnName = p.GetCustomAttribute<ColumnAttribute>()?.Name;
                return columnName ?? p.Name;
            });
            return string.Join(", ", nomesCampos);
        }

        private string ObterValoresInsert(T entidade)
        {
            var tipo = typeof(T);
            var propriedades = tipo.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => !Attribute.IsDefined(p, typeof(IgnoreDapperAttribute)));
            var nomesCampos = propriedades.Select(p => $"@{p.Name}");
            return string.Join(", ", nomesCampos);
        }

        private string ObterCamposUpdate(T entidade)
        {
            var tipo = typeof(T);
            var propriedades = tipo.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => !Attribute.IsDefined(p, typeof(IgnoreDapperAttribute)));
            var nomesCampos = propriedades.Select(p =>{
                var columnName = p.GetCustomAttribute<ColumnAttribute>()?.Name;
                return $"{columnName ?? p.Name} = @{p.Name}";
            });
            return string.Join(", ", nomesCampos);
        }

        public void Atualizar(T entidade)
        {
            var campos = ObterCamposUpdate(entidade);
            var sql = $"UPDATE {_nomeTabela} SET {campos} WHERE Id = @Id";
            _conexao.Execute(sql, entidade);
        }

        public void Excluir(int id)
        {
            var sql = $"DELETE FROM {_nomeTabela} WHERE Id = @Id";
            _conexao.Execute(sql, new { Id = id });
        }

        public void Inserir(T entidade)
        {
            var campos = ObterCamposInsert(entidade);
            var valores = ObterValoresInsert(entidade);
            var sql = $"INSERT INTO {_nomeTabela} ({campos}) VALUES ({valores})";
            _conexao.Execute(sql, entidade);
        }

        public T? ObterPorId(int id)
        {
            var sql = $"SELECT * FROM {_nomeTabela} where Id = @Id";
            return _conexao.QueryFirstOrDefault<T>(sql, new { Id = id });
        }

        public IEnumerable<T> ObterTodos()
        {
            var sql = $"SELECT * FROM {_nomeTabela}";
            return _conexao.Query<T>(sql);
        }
    }
}