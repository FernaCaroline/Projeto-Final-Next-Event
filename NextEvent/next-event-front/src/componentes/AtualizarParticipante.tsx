import { useEffect, useState } from "react";
import Participante from "../interfaces/Participante";
import axios from "axios";
import { useParams, useNavigate } from "react-router-dom";

function AtualizarParticipante() {
    const { id } = useParams();
    const navigate = useNavigate();

    const [nome, setNome] = useState("");
    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");

    //Carrega participante ao entrar na página
    useEffect(() => {
        buscarParticipanteAPI();
    }, []);

    async function buscarParticipanteAPI() {
        try {
            const resposta = await axios.get<Participante>(
                `http://localhost:5162/api/participante/buscar/${id}`
            );

            setNome(resposta.data.nome);
            setEmail(resposta.data.email);
        } catch (error) {
            console.log("Erro ao buscar participante: " + error);
        }
    }

    function submeterForm(e: any) {
        e.preventDefault();
        enviarAtualizacaoAPI();
    }

    async function enviarAtualizacaoAPI() {
        try {
            const participanteAtualizado = {
                nome,
                email,
                senha
            };

            await axios.put(
                `http://localhost:5162/api/participante/atualizar/${id}`,
                participanteAtualizado
            );

            navigate("/participante/listar");
        } catch (error) {
            console.log("Erro ao atualizar: " + error);
        }
    }

    return (
        <div>
            <h1>Atualizar Participante</h1>

            <form onSubmit={submeterForm}>
                <div>
                    <label>Nome:</label>
                    <input
                        value={nome}
                        onChange={(e) => setNome(e.target.value)}
                    />
                </div>

                <div>
                    <label>Email:</label>
                    <input
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                </div>

                <div>
                    <label>Senha:</label>
                    <input
                        type="password"
                        onChange={(e) => setSenha(e.target.value)}
                    />
                </div>

                <button type="submit">Salvar</button>
            </form>
        </div>
    );
}

export default AtualizarParticipante;
