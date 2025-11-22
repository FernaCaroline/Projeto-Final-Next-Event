import { useState } from "react";
import Evento from "../interfaces/Evento";
import axios from "axios";

function BuscarEventoID() {
  const [id, setId] = useState("");
  const [evento, setEvento] = useState<Evento | null>(null);
  const [erro, setErro] = useState("");

  async function buscarEventoAPI() {
    try {
      const resposta = await axios.get<Evento>(
        `http://localhost:5162/api/evento/buscar/${id}`
      );
      setEvento(resposta.data);
      setErro("");
    } catch (error) {
      setEvento(null);
      setErro("Evento não encontrado.");
    }
  }

  return (
    <div>
      <h1>Buscar Evento por ID</h1>

      <input
        type="number"
        placeholder="Digite o ID"
        value={id}
        onChange={(e) => setId(e.target.value)}
      />

      <button onClick={buscarEventoAPI}>Buscar</button>

      {erro && <p>{erro}</p>}

      {evento && (
        <table>
          <thead>
            <tr>
              <th>Id</th>
              <th>Nome</th>
              <th>Descrição</th>
              <th>Data Início</th>
              <th>Data Fim</th>
            </tr>
          </thead>

          <tbody>
            <tr>

              <td>{evento.id}</td>
              <td>{evento.nome}</td>
              <td>{evento.descricao}</td>
              <td>{evento.dataInicio}</td>
              <td>{evento.dataFim}</td>
            </tr>
          </tbody>
        </table>
      )}
    </div>
  );
}

export default BuscarEventoID;
