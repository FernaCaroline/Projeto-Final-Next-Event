import { useEffect, useState } from "react";
import Evento from "../interfaces/Evento";
import axios from "axios";

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
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ListarEventos;