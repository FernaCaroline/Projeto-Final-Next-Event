import { useEffect, useState } from "react";
import Participante from "../interfaces/Participante";
import axios from "axios";
import { Link } from "react-router-dom";

function ListarParticipante() {
    const [participantes, setParticipantes] = useState<Participante[]>([]);

    //useEffect é pra executar um código no carregamento do componente
    useEffect(() => {
        listarParticipantesAPI();
    }, []);

    //AXIOS para listar do banco
    async function listarParticipantesAPI() {
        try {
            const resposta = await axios.get<Participante[]>(
                "http://localhost:5162/api/participante/listar"
            );
            setParticipantes(resposta.data);
        } catch (error) {
            console.log("Erro ao listar participantes: " + error);
        }
    }

    //Deletar participante
    async function deletarParticipante(id: string) {
        try {
            await axios.delete(
                `http://localhost:5162/api/participante/deletar/${id}`
            );
            listarParticipantesAPI(); //Atualiza a lista
        } catch (error) {
            console.log("Erro ao deletar participante: " + error);
        }
    }

    return (
        <div>
            <h1>Listar Participantes</h1>

            <table>
                <thead>
                <tr>
                    <th>Id</th>
                    <th>Nome</th>
                    <th>Email</th>
                    <th>Criado Em</th>
                    <th>Deletar</th>
                    <th>Editar</th>
                </tr>
                </thead>

                <tbody>
                {participantes.map((p) => (
                    <tr key={p.id}>
                        <td>{p.id}</td>
                        <td>{p.nome}</td>
                        <td>{p.email}</td>
                        <td>{p.criadoEm}</td>
                        <td>
                            <button onClick={() => deletarParticipante(p.id!)}>
                                Deletar
                            </button>
                        </td>
                        <td>
                            {/* Igual ao professor — vai para a tela de alterar */}
                            <Link to={`/participante/atualizar/${p.id}`}>
                                Atualizar
                            </Link>
                        </td>
                    </tr>
                ))}
                </tbody>
            </table>
        </div>
    );
}

export default ListarParticipante;
