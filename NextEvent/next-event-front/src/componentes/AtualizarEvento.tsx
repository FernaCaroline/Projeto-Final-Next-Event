import { useEffect, useState } from "react";
import Evento from "../interfaces/Evento";
import axios from "axios";
import { useParams, useNavigate } from "react-router-dom";

function AtualizarEvento() {
    const { id } = useParams();
    const navigate = useNavigate();

    const [nome, setNome] = useState("");
    const [descricao, setDescricao] = useState("");
    const [dataInicio, setDataInicio] = useState("");
    const [dataFim, setDataFim] = useState("");

    useEffect(() => {
        buscarEventoAPI();
    }, []);

    async function buscarEventoAPI() {
        try {
            const resposta = await axios.get<Evento>(
                `http://localhost:5162/api/evento/buscar/${id}`
            );

            setNome(resposta.data.nome);
            setDescricao(resposta.data.descricao);
            setDataInicio(resposta.data.dataInicio);
            setDataFim(resposta.data.dataFim);

        } catch (error) {
            console.log("Erro ao buscar evento: " + error);
        }
    }

    function submeterForm(e: any) {
        e.preventDefault();
        enviarAtualizacao();
    }

    async function enviarAtualizacao() {
        try {
            const eventoAtualizado = { nome, descricao, dataInicio, dataFim };

            const resposta = await axios.patch(
                `http://localhost:5162/api/evento/atualizar/${id}`,
                eventoAtualizado
            );

            console.log(resposta.data);
            navigate("/eventos/listar");

        } catch (error) {
            console.log("Erro na requisição: " + error);
        }
    }

    return (
        <div>
            <h1>Atualizar Evento</h1>
            <form onSubmit={submeterForm}>
                <div>
                    <label>Nome:</label>
                    <input value={nome} onChange={(e) => setNome(e.target.value)} />
                </div>
                <div>
                    <label>Descrição:</label>
                    <input value={descricao} onChange={(e) => setDescricao(e.target.value)} />
                </div>
                <div>
                    <label>Data Início:</label>
                    <input value={dataInicio} type="datetime-local"
                           onChange={(e) => setDataInicio(e.target.value)} />
                </div>
                <div>
                    <label>Data Fim:</label>
                    <input value={dataFim} type="datetime-local"
                           onChange={(e) => setDataFim(e.target.value)} />
                </div>

                <button type="submit">Salvar</button>
            </form>
        </div>
    );
}

export default AtualizarEvento;
