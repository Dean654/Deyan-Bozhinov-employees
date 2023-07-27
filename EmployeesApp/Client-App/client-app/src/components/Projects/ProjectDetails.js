const ProjectDetails = (props) => {
  const { project } = props;
  return (
    <tr>
      <td>{project.firstEmployeeId}</td>
      <td>{project.secondEmployeeId !==0 ? project.secondEmployeeId: null}</td>
      <td>{project.projectId}</td>
      <td>{project.totalDays}</td>
    </tr>
  );
};

export default ProjectDetails;
