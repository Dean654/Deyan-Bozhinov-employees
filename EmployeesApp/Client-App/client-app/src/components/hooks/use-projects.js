import { useState } from "react";

const useManageProjects = () => {
  const [projects, setProjects] = useState([]);

  const uploadFile = async (file) => {
    if (!file) {
      alert("Please select a file.");
      return;
    }

    const formData = new FormData();
    formData.append("formFile", file);

    const response = await fetch("https://localhost:7077/Project", {
      method: "POST",
      body: formData,
    });

    if (!response.ok) {
      alert("Something goes wrong!");
    }

    const result = await response.json();

    setProjects(result);
  };

  return { uploadFile: uploadFile, projects, setProjects };
};

export default useManageProjects;
