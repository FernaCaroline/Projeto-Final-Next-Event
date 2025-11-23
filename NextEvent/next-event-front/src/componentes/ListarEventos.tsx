import { useEffect, useState } from "react";
import Evento from "../interfaces/Evento";
import axios from "axios";
import { Link } from "react-router-dom"; // já estava no outro

//Anna

function ListarEventos() {
  const [eventos, setEventos] = useState<Evento[]>([]);

  //useEffect é pra executar um código no carregamento do componente
  useEffect(() => {
    listarEventosAPI();
  }, []);

  //Isso aqui é oq puxa la do BD, usando o AXIOS
  async function listarEventosAPI() {
    try {
      const resposta = await axios.get<Evento[]>(
          "http://localhost:5162/api/evento/listar"
      );
      const dados = resposta.data;
      setEventos(dados);
    } catch (error) {
      console.log(error);
    }
  }

  function deletarEvento(id: number) {
    deletarEventoAPI(id);
  }

  async function deletarEventoAPI(id: number) {
    try {
      await axios.delete(`http://localhost:5162/api/evento/deletar/${id}`);
      listarEventosAPI(); // atualiza tabela igual ao professor
    } catch (error) {
      console.log("Erro ao deletar evento: " + error);
    }
  }

  return (
      <div id="component_listar_eventos">
        <h1>Listar Eventos</h1>
        <table>
          <thead>
          <tr>
            <th>Id</th>
            <th>Nome</th>
            <th>Descrição</th>
            <th>Início das inscrições</th>
            <th>Fim das inscrições</th>
            {/* Aqui coloquei os dados que acho relevantes para o usuário final, adptando os nome para que façam sentido com o contexto */}
            <th>Editar</th>
            <th>Deletar</th>
          </tr>
          </thead>
          <tbody>
          {eventos.map((evento) => (
              <tr key={evento.id}>
                <td>{evento.id}</td>
                <td>{evento.nome}</td>
                <td>{evento.descricao}</td>
                <td>{evento.dataInicio}</td>
                <td>{evento.dataFim}</td>

                <td>
                  <Link to={`/eventos/atualizar/${evento.id}`}>
                    Atualizar
                  </Link>
                </td>

                <td>
                  <button onClick={() => deletarEvento(evento.id!)}>
                    Deletar
                  </button>
                </td>

              </tr>
          ))}
          </tbody>
        </table>
      </div>
  );
}

export default ListarEventos;
